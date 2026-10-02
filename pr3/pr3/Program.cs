namespace pr3
{
    public class CompanySystem
    {
        private List<string> _departments = new List<string>();
        private List<string> _employees = new List<string>();
        private Dictionary<string, List<string>> _projects = new Dictionary<string, List<string>>();

        public void ManageStructure(string action, string departmentName)
        {
            if (action == "CREATE")
            {
                _departments.Add(departmentName);
                Console.WriteLine($"[Управління структурою] Створено новий відділ: {departmentName}");
            }
        }

        public void ManagePersonnel(string employeeName, decimal salary)
        {
            _employees.Add(employeeName);
            Console.WriteLine($"[HR-модуль] Зареєстровано нового співробітника: {employeeName}. Оклад: {salary} грн.");
        }

        public void AssignToProject(string projectName, string employeeName)
        {
            if (!_projects.ContainsKey(projectName))
            {
                _projects[projectName] = new List<string>();
                Console.WriteLine($"[Управління проєктами] Ініційовано новий проєкт: {projectName}");
            }

            _projects[projectName].Add(employeeName);
            Console.WriteLine($"[Управління проєктами] Співробітника {employeeName} призначено на проєкт '{projectName}'.");
        }

        public void RegisterContract(string contractNumber, string signatoryName)
        {
            Console.WriteLine($"[Контракти] Зареєстровано контракт {contractNumber}, підписант: {signatoryName}.");
        }

        public void GenerateReport()
        {
            Console.WriteLine("\n=== АНАЛІТИЧНИЙ ЗВІТ КОМПАНІЇ ===");
            Console.WriteLine($"Всього відділів: {_departments.Count}");
            Console.WriteLine($"Всього співробітників у штаті: {_employees.Count}");
            Console.WriteLine($"Активних проєктів: {_projects.Count}");
            Console.WriteLine("=================================");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Ініціалізація системи автоматизації роботи компанії...\n");

            var system = new CompanySystem();

            system.ManageStructure("CREATE", "IT Відділ");
            system.ManageStructure("CREATE", "Відділ Маркетингу");

            Console.WriteLine();
            system.ManagePersonnel("Безручко В. С.", 30000m);
            system.ManagePersonnel("Петренко П. П.", 28000m);

            Console.WriteLine();
            system.RegisterContract("CTR-2026-01", "Безручко В. С.");

            Console.WriteLine();
            system.AssignToProject("Мобільний Додаток", "Безручко В. С.");
            system.AssignToProject("Мобільний Додаток", "Петренко П. П.");

            system.GenerateReport();

            Console.ReadLine();
        }
    }
}
