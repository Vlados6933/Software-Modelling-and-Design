using System;
using System.Collections.Generic;

namespace CompanyCommunicationApp
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal BaseSalary { get; set; }

        public decimal CalculatePay()
        {
            return BaseSalary;
        }
    }

    public class Department
    {
        public List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { Name = "В. Безручко", BaseSalary = 45000m },
                new Employee { Name = "О. Петренко", BaseSalary = 38000m }
            };
        }
    }

    public class BankAPI
    {
        public void TransferFunds(decimal totalAmount)
        {
            Console.WriteLine($"Успішно переказано кошти до банку. Сума: {totalAmount} грн.");
        }
    }

    public class PayrollController
    {
        private Department _department = new Department();
        private BankAPI _bankApi = new BankAPI();

        public void ProcessPayroll()
        {
            Console.WriteLine("Початок процесу нарахування заробітної плати...");

            var employees = _department.GetEmployees();
            decimal totalPayroll = 0;

            foreach (var emp in employees)
            {
                decimal pay = emp.CalculatePay();
                totalPayroll += pay;
                Console.WriteLine($"Працівник {emp.Name} -> нараховано {pay} грн.");
            }

            if (totalPayroll > 0)
            {
                _bankApi.TransferFunds(totalPayroll);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var controller = new PayrollController();
            controller.ProcessPayroll();

            Console.ReadLine();
        }
    }
}