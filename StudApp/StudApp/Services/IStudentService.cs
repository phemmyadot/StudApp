﻿using StudApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudApp.Services
{
    internal interface IStudentService
    {
        Task<List<Student>> GetAllStudents();

        Task AddNewStudent(Student student);
        Task DeleteStudent(string studentId);

    }
}
