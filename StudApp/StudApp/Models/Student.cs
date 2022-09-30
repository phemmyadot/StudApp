using FreshMvvm;
using System;
using System.Text.Json.Serialization;

namespace StudApp.Models
{
    public class Student 
    {
        public string id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string dateOfBirth { get; set; }

        public string comment { get; set; }

        public int gender { get; set; }

        [JsonIgnore]
        public string fullName => $"{firstName} {lastName}";
        [JsonIgnore]
        public string genderString => gender == 1 ? "Female" : "Male";
        [JsonIgnore]
        public string StudentImage => "https://media.istockphoto.com/vectors/user-icon-flat-isolated-on-white-background-user-symbol-vector-vector-id1300845620?k=20&m=1300845620&s=612x612&w=0&h=f4XTZDAv7NPuZbG0habSpU0sNgECM0X7nbKzTUta3n8=";

    }
}