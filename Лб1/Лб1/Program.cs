using System.Text;

namespace Лб1
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }

    public class GroupSubject
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
    }

    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _data = new List<T>();

        public IEnumerable<T> GetAll() => _data;

        public void Add(T entity) => _data.Add(entity);
    }

    public class UniversityDatabase
    {
        public IRepository<Student> Students { get; } = new InMemoryRepository<Student>();
        public IRepository<Group> Groups { get; } = new InMemoryRepository<Group>();
        public IRepository<Subject> Subjects { get; } = new InMemoryRepository<Subject>();
        public IRepository<Teacher> Teachers { get; } = new InMemoryRepository<Teacher>();
        public IRepository<GroupSubject> GroupSubjects { get; } = new InMemoryRepository<GroupSubject>();

        public void SeedData()
        {
            var teacher1 = new Teacher { Id = 1, FullName = "Brad Pit" };
            var teacher2 = new Teacher { Id = 2, FullName = "Silvester Stalone" };
            Teachers.Add(teacher1);
            Teachers.Add(teacher2);

            var subject1 = new Subject { Id = 1, Title = "Software Engineering" };
            subject1.Teachers.Add(teacher1);
            subject1.Teachers.Add(teacher2);
            Subjects.Add(subject1);

            var group1 = new Group { Id = 1, Name = "ПД-31" };
            Groups.Add(group1);

            var groupSubject = new GroupSubject { GroupId = group1.Id, Group = group1, SubjectId = subject1.Id, Subject = subject1 };
            GroupSubjects.Add(groupSubject);
            group1.GroupSubjects.Add(groupSubject);

            var student1 = new Student { Id = 1, LastName = "Padaleski", GroupId = group1.Id, Group = group1 };
            var student2 = new Student { Id = 2, LastName = "Potter", GroupId = group1.Id, Group = group1 };
            Students.Add(student1);
            Students.Add(student2);
        }
    }

    public class StudentTeacherServicesBLL
    {
        private readonly UniversityDatabase _db;

        public StudentTeacherServicesBLL(UniversityDatabase db)
        {
            _db = db;
        }

        public List<Teacher> FindTeachersByStudentSurname(string lastName)
        {
            var student = _db.Students.GetAll().FirstOrDefault(s => s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (student == null) return new List<Teacher>();

            var group = _db.Groups.GetAll().FirstOrDefault(g => g.Id == student.GroupId);

            if (group == null) return new List<Teacher>();

            var groupSubjects = _db.GroupSubjects.GetAll().Where(gs => gs.GroupId == group.Id).Select(gs => gs.Subject).ToList();

            var teachers = new List<Teacher>();
            foreach (var subject in groupSubjects)
            {
                teachers.AddRange(subject.Teachers);
            }

            return teachers.Distinct().ToList();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var db = new UniversityDatabase();
            db.SeedData();
            var servicesBLL = new StudentTeacherServicesBLL(db);

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nPersonnel Department service menu");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Get Subject Teacher");
                Console.WriteLine("2. Find Teachers by Student surname");
                Console.Write("-> ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        isRunning = false;
                        break;
                    case "1":
                        Console.WriteLine("Цей функціонал в розробці...");
                        break;
                    case "2":
                        Console.Write("Введіть прізвище студента (наприклад, Padaleski): ");
                        string surname = Console.ReadLine();

                        var teachers = servicesBLL.FindTeachersByStudentSurname(surname);

                        if (teachers.Any())
                        {
                            Console.WriteLine($"\nВикладачі для студента {surname}:");
                            foreach (var teacher in teachers)
                            {
                                Console.WriteLine($"- {teacher.Id} {teacher.FullName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nСтудента не знайдено або у нього немає викладачів.");
                        }
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
