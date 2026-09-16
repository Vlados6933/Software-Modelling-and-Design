namespace lb2
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public Student(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public override string ToString()
        {
            return $"{Id} - {FullName}";
        }
    }

    public class Group
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public Group(string name)
        {
            Name = name;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
    }

    public class Department
    {
        public string Name { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public Department(string name)
        {
            Name = name;
        }

        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Department softwareEngineering = new Department("Кафедра інженерії програмного забезпечення");
            Group pd31 = new Group("ПД-32");

            pd31.AddStudent(new Student(1, "Безручко Владислав Сергійович"));
            pd31.AddStudent(new Student(2, "Петренко Петро Петрович"));

            softwareEngineering.AddGroup(pd31);

            Console.WriteLine($"Відділення: {softwareEngineering.Name}");
            foreach (var group in softwareEngineering.Groups)
            {
                Console.WriteLine($" Група: {group.Name}");
                foreach (var student in group.Students)
                {
                    Console.WriteLine($"  Студент: {student}");
                }
            }

            Console.ReadLine();
        }
    }
}
