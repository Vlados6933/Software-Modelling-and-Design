using System;
using System.Collections.Generic;

namespace UniversityClassDiagramApp
{
    public interface IPerson
    {
        string GetDetails();
    }

    public abstract class Person : IPerson
    {
        private int _id;
        private string _name;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public abstract string GetDetails();
    }

    public class Student : Person
    {
        private string _studentCard;

        public string StudentCard
        {
            get => _studentCard;
            set => _studentCard = value;
        }

        public override string GetDetails()
        {
            return $"Студент: {Name}, Квиток: {StudentCard}";
        }
    }

    public class Teacher : Person
    {
        private decimal _salary;

        public decimal Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public override string GetDetails()
        {
            return $"Викладач: {Name}, Оклад: {Salary} грн";
        }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Teacher CourseTeacher { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public void AddStudent(Student student)
        {
            EnrolledStudents.Add(student);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Teacher teacher = new Teacher { Id = 1, Name = "Іванов І.І.", Salary = 25000m };
            Student student1 = new Student { Id = 101, Name = "Безручко В.С.", StudentCard = "KB-123" };
            Student student2 = new Student { Id = 102, Name = "Петренко П.П.", StudentCard = "KB-124" };

            Course oopCourse = new Course
            {
                CourseId = 1,
                Title = "Об'єктно-орієнтоване програмування",
                CourseTeacher = teacher
            };

            oopCourse.AddStudent(student1);
            oopCourse.AddStudent(student2);

            List<IPerson> universityPeople = new List<IPerson> { teacher, student1, student2 };

            Console.WriteLine("=== Учасники навчального процесу ===");
            foreach (var person in universityPeople)
            {
                Console.WriteLine(person.GetDetails());
            }

            Console.WriteLine($"\n=== Курс: {oopCourse.Title} ===");
            Console.WriteLine($"Викладає: {oopCourse.CourseTeacher.Name}");
            Console.WriteLine("Записані студенти:");

            foreach (var s in oopCourse.EnrolledStudents)
            {
                Console.WriteLine($"- {s.Name}");
            }

            Console.ReadLine();
        }
    }
}