using FreshMvvm;
using StudApp.Models;
using StudApp.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudApp.PageModels
{
    internal class NewStudentPageModel : FreshMvvm.FreshBasePageModel
    {
        private StudentService _service = FreshIOC.Container.Resolve<StudentService>();
        private Student _student;
        public string StudentId;
        public Student Student
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }
        private string _buttonText;
        private string _pageTitle;
        public string ButtonText { get => _buttonText; set => _buttonText = value; }
        public string PageTitle { get => _pageTitle; set => _pageTitle = value; }

        public bool IsEdit = false;


        public override void Init(object initData)
        {
            base.Init(initData);
            getStudent();

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {

        }
        public override void ReverseInit(object returnedData)
        {
            getStudent();
            base.ReverseInit(returnedData);
        }


        public void getStudent()
        {
            IsEdit = StudentId != null;
            ButtonText = IsEdit ? "Edit" : "Add";
            PageTitle = IsEdit ? "Edit Student" : "Add new student";
            if (IsEdit)
            {
                Task<Student> getStudent = _service.GetStudent(StudentId);
                getStudent.Wait();
                Student = getStudent.Result;
            }
            else
            {
                Student = new Student()
                {
                    id = StudentId ?? Guid.NewGuid().ToString(),
                    gender = 0,
                    dateOfBirth = DateTime.UtcNow.ToString("s") + "Z",
                };
            }

            Debug.Write(Student.firstName);
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsEdit)
                    {
                        await _service.UpdateStudent(Student);
                    }
                    else
                    {
                        await _service.AddNewStudent(Student);
                    }
                    await CoreMethods.PopPageModel();
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
