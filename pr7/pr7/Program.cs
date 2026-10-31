using System;
using System.Collections.Generic;

namespace CompanyComponentApp
{
    public interface IDataRepository
    {
        List<string> GetEmployees();
    }

    public interface ICompanyService
    {
        void ProcessPayroll();
    }

    public class DataAccessComponent : IDataRepository
    {
        public List<string> GetEmployees()
        {
            return new List<string>
            {
                "В. Безручко",
                "О. Петренко",
                "І. Іванов"
            };
        }
    }

    public class BusinessLogicComponent : ICompanyService
    {
        private readonly IDataRepository _repository;

        public BusinessLogicComponent(IDataRepository repository)
        {
            _repository = repository;
        }

        public void ProcessPayroll()
        {
            var employees = _repository.GetEmployees();

            foreach (var emp in employees)
            {
                Console.WriteLine($"Нараховано заробітну плату працівнику: {emp}");
            }
        }
    }

    public class UIComponent
    {
        private readonly ICompanyService _companyService;

        public UIComponent(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public void RunApplication()
        {
            Console.WriteLine("=== Система Компанії (Компонентна Архітектура) ===");
            _companyService.ProcessPayroll();
            Console.WriteLine("==================================================");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IDataRepository dataComponent = new DataAccessComponent();
            ICompanyService businessComponent = new BusinessLogicComponent(dataComponent);
            UIComponent uiComponent = new UIComponent(businessComponent);

            uiComponent.RunApplication();

            Console.ReadLine();
        }
    }
}