using FreshMvvm;
using StudApp.Models;
using StudApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace StudApp.PageModels
{
    internal class NewStudentPageModel : FreshMvvm.FreshBasePageModel
    {
        private StudentService _service = FreshIOC.Container.Resolve<StudentService>();
        private Student _student;
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
        public NewStudentPageModel()
        {

            Student = new Student()
            {
                id = Guid.NewGuid().ToString(),
                gender = 0,
                dateOfBirth = DateTime.UtcNow.ToString("s") + "Z",
            };
        }
       
        public Command SaveCommand
        {
            get
            {
                return new Command(async () =>{
                    await _service.AddNewStudent(Student);
                    await CoreMethods.PopPageModel();
                }
                   
                );
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
