using FreshMvvm;
using StudApp.Models;
using StudApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudApp.PageModels
{
    internal class DashboardPageModel : FreshMvvm.FreshBasePageModel
    {
        private StudentService _service = FreshIOC.Container.Resolve<StudentService>();
        private Student _selectedStudent = null;
        public bool _isEnabled = false;

        public ObservableCollection<Student> Students { get; private set; }

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



        private void LoadItems()
        {
            Students.Clear();
            Task<List<Student>> getStudents = _service.GetAllStudents();
            getStudents.Wait();
            foreach (var student in getStudents.Result)
            {
                Students.Add(student);
                Console.Write(student.firstName + student.lastName);
            }
        }




        public override void Init(object initData)
        {
            base.Init(initData);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            LoadItems();
        }
        public override void ReverseInit(object returnedData)
        {
            LoadItems();
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

        public Command EditStudent
        {
            get
            {

                return new Command(() =>
                {


                });
            }
        }
        public Command DeleteStudent
        {
            get
            {

                return new Command(async () =>
                {

                    bool accepted = await Application.Current.MainPage.DisplayAlert("Alert", "Are you sure you want to delete?", "Yes", "Cancel");
                    if (accepted)
                    {
                        await _service.DeleteStudent(SelectedStudent.id);
                        SelectedStudent = null;
                        LoadItems();
                    }
                });
            }
        }


        #region Commands

        #endregion
    }
}
