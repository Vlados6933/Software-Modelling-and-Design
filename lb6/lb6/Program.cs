using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversitySequenceApp
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int MaxStudents { get; set; }
        public List<int> EnrolledStudents { get; set; } = new List<int>();

        public bool AddStudent(int studentId)
        {
            EnrolledStudents.Add(studentId);
            return true;
        }
    }

    public class CourseRepository
    {
        private List<Course> _courses;

        public CourseRepository()
        {
            _courses = new List<Course>
            {
                new Course { CourseId = 1, Title = "OOP", MaxStudents = 2 },
                new Course { CourseId = 2, Title = "Databases", MaxStudents = 1 }
            };
        }

        public Course GetCourse(int courseId)
        {
            return _courses.FirstOrDefault(c => c.CourseId == courseId);
        }
    }

    public class EnrollmentController
    {
        private CourseRepository _repository;

        public EnrollmentController(CourseRepository repository)
        {
            _repository = repository;
        }

        public string Enroll(int courseId, int studentId)
        {
            Console.WriteLine($"-> Student {studentId} requested enrollment in course {courseId}");

            Course course = _repository.GetCourse(courseId);

            if (course == null)
            {
                return "<- Error: Course not found\n";
            }

            if (course.EnrolledStudents.Count >= course.MaxStudents)
            {
                return "<- Error: Course full\n";
            }

            course.AddStudent(studentId);
            return "<- Success: Enrolled\n";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CourseRepository repo = new CourseRepository();
            EnrollmentController controller = new EnrollmentController(repo);

            Console.WriteLine(controller.Enroll(1, 101));
            Console.WriteLine(controller.Enroll(1, 102));
            Console.WriteLine(controller.Enroll(1, 103));

            Console.ReadLine();
        }
    }
}