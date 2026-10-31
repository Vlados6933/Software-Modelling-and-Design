using System;
using System.Collections.Generic;

namespace UniversityApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

namespace UniversityApp.DAL
{
    using UniversityApp.Models;

    public class StudentRepository
    {
        private List<Student> _students = new List<Student>();

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public List<Student> GetAll()
        {
            return _students;
        }
    }
}

namespace UniversityApp.BLL
{
    using UniversityApp.Models;
    using UniversityApp.DAL;

    public class UniversityService
    {
        private StudentRepository _repository;

        public UniversityService()
        {
            _repository = new StudentRepository();
        }

        public void RegisterStudent(int id, string name)
        {
            var student = new Student { Id = id, Name = name };
            _repository.Add(student);
        }

        public void PrintAllStudents()
        {
            var students = _repository.GetAll();
            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.Id} | Студент: {s.Name}");
            }
        }
    }
}

namespace UniversityApp.UI
{
    using UniversityApp.BLL;

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var service = new UniversityService();

            service.RegisterStudent(101, "В. Безручко");
            service.RegisterStudent(102, "О. Петренко");

            Console.WriteLine("=== База даних Університету ===");
            service.PrintAllStudents();

            Console.ReadLine();
        }
    }
}