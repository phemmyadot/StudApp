using StudApp.AuthHelpers;
using StudApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace StudApp.Services
{
    internal class StudentService : IStudentService
    {
        public string StatusMessage { get; set; }

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public StudentService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task AddNewStudent(Student student)
        {
            try
            {
                Uri uri = new Uri(string.Format(Constants.baseUrl + "Student", string.Empty));
                string json = JsonSerializer.Serialize<Student>(student, serializerOptions);



                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Student successfully saved.");
                }
                else
                {
                    Debug.WriteLine("Error creating student");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to create student: {student.firstName}. Error: {ex.Message}";
            }

        }

        public async Task UpdateStudent(Student student)
        {
            try
            {
                Uri uri = new Uri(string.Format(Constants.baseUrl + "Student/"+ student.id, string.Empty));
                string json = JsonSerializer.Serialize<Student>(student, serializerOptions);



                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                response = await client.PutAsync(uri, content);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Student successfully updated.");
                }
                else
                {
                    Debug.WriteLine("Error updateing student");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update student: {student.firstName}. Error: {ex.Message}";
            }

        }
        public async Task DeleteStudent(string studentId)
        {
            try
            {
                Uri uri = new Uri(string.Format(Constants.baseUrl + "Student/" + studentId, string.Empty));



                HttpResponseMessage response = null;

                response = await client.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Student successfully deleted.");
                }
                else
                {
                    Debug.WriteLine("Error deleting student");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete student. Error: {ex.Message}";
            }

        }

        public async Task<List<Student>> GetAllStudents()
        {
            List<Student> Students = new List<Student>();
            try
            {
                Uri uri = new Uri(string.Format(Constants.baseUrl + "Student", string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Students = JsonSerializer.Deserialize<List<Student>>(content, serializerOptions);
                }
                else
                {

                    Debug.WriteLine("Error fecthing students");
                }
                return Students;
            }
            catch (Exception e)
            {
                StatusMessage = $"{e.Message}";
                Debug.WriteLine(StatusMessage);
                return Students;
            }

        }
        public async Task<Student> GetStudent(string studentId)
        {
            Student Student = new Student();
            try
            {
                Uri uri = new Uri(string.Format(Constants.baseUrl + "Student/"+studentId, string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Student = JsonSerializer.Deserialize<Student>(content, serializerOptions);
                }
                else
                {

                    Debug.WriteLine("Error fecthing students");
                }
                return Student;
            }
            catch (Exception e)
            {
                StatusMessage = $"{e.Message}";
                Debug.WriteLine(StatusMessage);
                return Student;
            }

        }

    }
}
