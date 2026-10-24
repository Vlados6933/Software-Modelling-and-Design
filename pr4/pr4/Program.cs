namespace pr4
{
    public static class ErrorLogger
    {
        private static List<string> _errorLogs = new List<string>();

        public static void LogError(Exception ex, string context)
        {
            string logEntry = $"[ERROR] {DateTime.Now:dd.MM.yyyy HH:mm:ss} | Контекст: {context} | Помилка: {ex.Message}";
            _errorLogs.Add(logEntry);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Системне сповіщення: Сталася помилка. Дані збережено в журнал.");
            Console.ResetColor();
        }

        public static void PrintErrorLogs()
        {
            Console.WriteLine("\n--- ЖУРНАЛ ПОМИЛОК СИСТЕМИ ---");
            if (_errorLogs.Count == 0) Console.WriteLine("Помилок не виявлено.");
            foreach (var log in _errorLogs) Console.WriteLine(log);
            Console.WriteLine("------------------------------\n");
        }
    }

    public static class NavigationTracker
    {
        private static List<string> _visitLogs = new List<string>();

        public static void LogVisit(string userId, string location)
        {
            string logEntry = $"[NAV] {DateTime.Now:dd.MM.yyyy HH:mm:ss} | Користувач: {userId} | Відвідав: {location}";
            _visitLogs.Add(logEntry);
        }

        public static void PrintNavigationLogs()
        {
            Console.WriteLine("\n--- ЖУРНАЛ НАВІГАЦІЇ КОРИСТУВАЧІВ ---");
            foreach (var log in _visitLogs) Console.WriteLine(log);
            Console.WriteLine("-------------------------------------\n");
        }
    }

    public class CompanySystem
    {
        private string _currentUser;

        public CompanySystem(string currentUser)
        {
            _currentUser = currentUser;
        }

        public void OpenHrDashboard()
        {
            NavigationTracker.LogVisit(_currentUser, "Панель управління персоналом (HR Dashboard)");
            Console.WriteLine($"> {_currentUser} відкрив(ла) панель управління персоналом.");
        }

        public void OpenProjectManagement()
        {
            NavigationTracker.LogVisit(_currentUser, "Управління проєктами (Project Management)");
            Console.WriteLine($"> {_currentUser} відкрив(ла) розділ управління проєктами.");
        }

        public void CalculateBonuses(int totalBudget, int employeesCount)
        {
            NavigationTracker.LogVisit(_currentUser, "Модуль розрахунку бонусів (Calculate Bonuses)");
            Console.WriteLine($"> {_currentUser} запустив(ла) розрахунок бонусів.");

            try
            {
                int bonusPerEmployee = totalBudget / employeesCount;
                Console.WriteLine($"Бонус на кожного працівника: {bonusPerEmployee} грн.");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "CalculateBonuses Method");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Система автоматизації компанії (Модуль нефункціональних вимог) ===\n");

            var system = new CompanySystem("Admin_Bezruchko");

            system.OpenHrDashboard();
            system.OpenProjectManagement();

            system.CalculateBonuses(100000, 0);

            Console.WriteLine("\n=== АНАЛІЗ ЗІБРАНИХ ДАНИХ ===");
            NavigationTracker.PrintNavigationLogs();
            ErrorLogger.PrintErrorLogs();

            Console.ReadLine();
        }
    }
}
