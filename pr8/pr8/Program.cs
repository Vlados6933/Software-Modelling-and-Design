using System;
using System.Collections.Generic;

namespace CompanyObjectDiagramApp
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    public class Department
    {
        public string Title { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var dept1 = new Department { Title = "IT Відділ" };
            var emp1 = new Employee { Name = "В. Безручко", Salary = 45000m };
            var emp2 = new Employee { Name = "О. Петренко", Salary = 38000m };

            dept1.Employees.Add(emp1);
            dept1.Employees.Add(emp2);

            Console.WriteLine($"[Знімок системи] Відділ: {dept1.Title}");

            foreach (var emp in dept1.Employees)
            {
                Console.WriteLine($" - Працівник: {emp.Name}, Оклад: {emp.Salary} грн");
            }

            Console.ReadLine();
        }
    }
}