namespace Лб2
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }

    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    public class EmployeeRepository
    {
        private readonly List<Employee> _employees = new List<Employee>();

        public void Add(Employee employee)
        {
            if (employee != null && !_employees.Any(e => e.Id == employee.Id))
            {
                _employees.Add(employee);
                Console.WriteLine($"[Успіх] Працівника '{employee.Name}' додано.");
            }
            else
            {
                Console.WriteLine("[Помилка] Працівник вже існує або дані некоректні.");
            }
        }

        public void Delete(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                Console.WriteLine($"[Успіх] Працівника з ID {id} видалено.");
            }
            else
            {
                Console.WriteLine($"[Помилка] Працівника з ID {id} не знайдено.");
            }
        }

        public Employee Search(string name)
        {
            var employee = _employees.FirstOrDefault(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            if (employee != null)
            {
                Console.WriteLine($"[Успіх] Знайдено працівника: ID {employee.Id}, Ім'я: {employee.Name}");
            }
            else
            {
                Console.WriteLine($"[Помилка] Працівника з ім'ям '{name}' не знайдено.");
            }
            return employee;
        }

        public void PrintAll()
        {
            Console.WriteLine("\n--- Список усіх працівників ---");
            if (_employees.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            foreach (var emp in _employees)
            {
                Console.WriteLine($"ID: {emp.Id} | Ім'я: {emp.Name}");
                if (emp.Projects.Any())
                {
                    Console.WriteLine("   Проєкти:");
                    foreach (var proj in emp.Projects)
                    {
                        Console.WriteLine($"   - {proj.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("   Проєкти: Немає активних проєктів.");
                }
            }
            Console.WriteLine("-------------------------------\n");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var employeeRepo = new EmployeeRepository();

            var project1 = new Project { Id = 1, Title = "Розробка CRM системи" };
            var project2 = new Project { Id = 2, Title = "Міграція баз даних" };

            var emp1 = new Employee { Id = 1, Name = "Олексій" };
            emp1.Projects.Add(project1);
            emp1.Projects.Add(project2);

            var emp2 = new Employee { Id = 2, Name = "Марія" };
            emp2.Projects.Add(project1);

            var emp3 = new Employee { Id = 3, Name = "Влад" };

            Console.WriteLine(">>> Тестування: Додавання");
            employeeRepo.Add(emp1);
            employeeRepo.Add(emp2);
            employeeRepo.Add(emp3);

            Console.WriteLine("\n>>> Тестування: Вивід списку");
            employeeRepo.PrintAll();

            Console.WriteLine(">>> Тестування: Пошук");
            employeeRepo.Search("Марія");
            employeeRepo.Search("Іван");

            Console.WriteLine("\n>>> Тестування: Видалення");
            employeeRepo.Delete(3); 

            Console.WriteLine("\n>>> Фінальний стан репозиторію");
            employeeRepo.PrintAll();

            Console.ReadLine();
        }
    }
}
