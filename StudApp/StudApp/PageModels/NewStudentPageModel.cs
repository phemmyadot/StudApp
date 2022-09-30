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

        public string ButtonText { get => buttonText; set => buttonText = value; }

        public bool IsEdit = false;
        private string buttonText = "Add";

        public NewStudentPageModel()
        {


        }

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
