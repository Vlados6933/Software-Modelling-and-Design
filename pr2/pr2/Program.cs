namespace pr2
{
    public class Department
    {
        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Помилка: Назва відділу не може бути порожньою.");
                _title = value;
            }
        }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public Department(int id, string title)
        {
            Id = id;
            Title = title; 
        }
    }

    public class Employee
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0) throw new ArgumentException("Помилка: ID має бути більше 0.");
                _id = value;
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                    throw new ArgumentException("Помилка: Некоректне ім'я співробітника.");
                _fullName = value;
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 18)
                    throw new ArgumentException($"Помилка: Співробітник {FullName} має бути повнолітнім (>=18).");
                _age = value;
            }
        }

        private decimal _salary;
        public decimal Salary
        {
            get { return _salary; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Помилка: Заробітна плата не може бути від'ємною.");
                _salary = value;
            }
        }

        public Employee(int id, string fullName, int age, decimal salary)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Salary = salary;
        }

        public override string ToString()
        {
            return $"ID: {Id} | ПІБ: {FullName} | Вік: {Age} | ЗП: {Salary} грн";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                Console.WriteLine("=== Створення даних на основі Словника термінів ===");

                Department itDepartment = new Department(1, "IT Відділ");

                Employee emp1 = new Employee(1, "Безручко В. С.", 22, 25000m);
                Employee emp2 = new Employee(2, "Петренко П. П.", 30, 32000m);

                itDepartment.Employees.Add(emp1);
                itDepartment.Employees.Add(emp2);

                Console.WriteLine($"\nВідділ: {itDepartment.Title}");
                foreach (var emp in itDepartment.Employees)
                {
                    Console.WriteLine($" - {emp}");
                }

                Console.WriteLine("\n=== Демонстрація перевірки коректності (Validation) ===");

                Console.WriteLine("Спроба створити співробітника з віком 17 років...");
                Employee empInvalid = new Employee(3, "Іванов І. І.", 17, 10000m);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nЗавершення роботи.");
            Console.ReadLine();
        }
    }
}
