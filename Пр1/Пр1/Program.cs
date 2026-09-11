namespace Пр1
{
    public class Contract
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public int SignatoryId { get; set; }
        public Signatory Signatory { get; set; }
    }

    public class Signatory
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Department> Departments { get; set; } = new List<Department>();
    }

    public class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }

    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var signatory = new Signatory { Id = 1, FullName = "Іванов Іван Іванович" };
            var contract = new Contract { Id = 101, Number = "CTR-2026-001", SignatoryId = signatory.Id, Signatory = signatory };
            signatory.ContractId = contract.Id;
            signatory.Contract = contract;

            var company = new Company { Id = 1, Name = "TechGlobal LLC" };
            var dept1 = new Department { Id = 1, Title = "IT Відділ", CompanyId = company.Id, Company = company };
            var dept2 = new Department { Id = 2, Title = "HR Відділ", CompanyId = company.Id, Company = company };
            company.Departments.Add(dept1);
            company.Departments.Add(dept2);

            var emp1 = new Employee { Id = 1, Name = "Олексій" };
            var emp2 = new Employee { Id = 2, Name = "Марія" };

            var proj1 = new Project { Id = 1, Title = "Розробка CRM" };
            var proj2 = new Project { Id = 2, Title = "Оновлення сайту" };

            emp1.Projects.Add(proj1);
            emp1.Projects.Add(proj2);
            proj1.Employees.Add(emp1);
            proj2.Employees.Add(emp1);

            emp2.Projects.Add(proj1);
            proj1.Employees.Add(emp2);


            Console.WriteLine("=== Завдання 1: Відношення 1:1 (Контракт - Підписант) ===");
            Console.WriteLine($"Контракт №: {contract.Number} підписаний особою: {contract.Signatory.FullName}");
            Console.WriteLine($"Підписант: {signatory.FullName} має контракт №: {signatory.Contract.Number}\n");

            Console.WriteLine("=== Завдання 2: Відношення 1:N (Компанія - Відділ) ===");
            Console.WriteLine($"Компанія: {company.Name} містить наступні відділи:");
            foreach (var dept in company.Departments)
            {
                Console.WriteLine($"- {dept.Title}");
            }
            Console.WriteLine();

            Console.WriteLine("=== Завдання 3: Відношення M:N (Працівник - Проєкт) ===");
            var allEmployees = new List<Employee> { emp1, emp2 };
            foreach (var emp in allEmployees)
            {
                Console.WriteLine($"Працівник {emp.Name} залучений до проєктів:");
                foreach (var proj in emp.Projects)
                {
                    Console.WriteLine($"- {proj.Title}");
                }
            }

            Console.WriteLine("\nЗавершення роботи модуля.");
            Console.ReadLine();
        }
    }
}
