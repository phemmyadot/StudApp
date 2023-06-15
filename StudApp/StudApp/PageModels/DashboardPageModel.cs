using FreshMvvm;
using StudApp.Models;
using StudApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudApp.PageModels
{
    internal class DashboardPageModel : FreshMvvm.FreshBasePageModel
    {
        private StudentService _service = FreshIOC.Container.Resolve<StudentService>();
        private Student _selectedStudent = null;
        public bool _isEnabled = false;

        public ObservableCollection<Student> Students { get; private set; }
        private bool _isLoading = true;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                this.RaisePropertyChanged();

            }
        }
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                IsEnabled = value == null ? false : true;
                this.RaisePropertyChanged();

            }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                this.RaisePropertyChanged();

            }
        }



        public DashboardPageModel()
        {
            Students = new ObservableCollection<Student>();
        }



        private void LoadStudents()
        {
            Students.Clear();
            Task<List<Student>> getStudents = _service.GetAllStudents();
            getStudents.Wait();

            foreach (var student in getStudents.Result)
            {
                Students.Add(student);
                Console.Write(student.firstName + student.lastName);
            }
            IsLoading = false;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            LoadStudents();
        }
        public override void ReverseInit(object returnedData)
        {
            LoadStudents();
            base.ReverseInit(returnedData);
        }

        public Command onClickAdd
        {
            get
            {

                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<NewStudentPageModel>();
                });
            }
        }
        public Command OnLogout
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    await SecureStorage.SetAsync("token", "");
                    await CoreMethods.PushPageModel<LoginPageModel>();
                    IsLoading = false;
                });
            }
        }

        public Command EditStudent
        {
            get
            {

                return new Command(async () =>
                {
                    IsLoading = true;
                    await CoreMethods.PushPageModel<NewStudentPageModel>(pm => pm.StudentId = SelectedStudent.id);
                    IsLoading = false;
                });
            }
        }
        public Command DeleteStudent
        {
            get
            {

                return new Command(async () =>
                {

                    IsLoading = true;
                    bool accepted = await Application.Current.MainPage.DisplayAlert("Alert", "Are you sure you want to delete?", "Yes", "Cancel");
                    if (accepted)
                    {
                        await _service.DeleteStudent(SelectedStudent.id);
                        SelectedStudent = null;
                        LoadStudents();
                    }
                    IsLoading = false;
                });
            }
        }


        #region Commands

        #endregion
    }
}
