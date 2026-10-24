using System.Diagnostics;

namespace lb4
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine($"[LOG {DateTime.Now:HH:mm:ss}] {message}");
        }

        public static void LogError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR {DateTime.Now:HH:mm:ss}] {error}");
            Console.ResetColor();
        }
    }

    public class UniversitySystemCore
    {
        public string SystemVersion { get; private set; } = "v1.0.0-release";

        public void InitializeSystem()
        {
            Logger.Log("Запуск ядра Системи автоматизації університету...");
            try
            {
                ConnectToDatabase();
                Logger.Log("Підключення до бази даних встановлено успішно.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Критичний збій системи: {ex.Message}");
            }
        }

        private void ConnectToDatabase()
        {
            Thread.Sleep(500);
        }

        public void ExecuteHighLevelFeature(string featureName)
        {
            Logger.Log($"Ініційовано запит до підсистеми: {featureName}");

            Stopwatch timer = new Stopwatch();
            timer.Start();

            SimulateWorkload();

            timer.Stop();

            if (timer.ElapsedMilliseconds < 2000)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PERFORMANCE OK] Операцію '{featureName}' виконано за {timer.ElapsedMilliseconds} мс.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[PERFORMANCE WARNING] Операція '{featureName}' перевищила ліміт часу: {timer.ElapsedMilliseconds} мс.");
                Console.ResetColor();
            }
        }

        private void SimulateWorkload()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(300, 1000));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Демонстрація модуля на основі Бачення та Специфікації ===\n");

            var universitySystem = new UniversitySystemCore();

            universitySystem.InitializeSystem();
            Console.WriteLine();

            universitySystem.ExecuteHighLevelFeature("Автентифікація користувача");
            universitySystem.ExecuteHighLevelFeature("Генерація розкладу занять");
            universitySystem.ExecuteHighLevelFeature("Синхронізація з банківською системою");

            Console.WriteLine("\nЗавершення роботи модуля.");
            Console.ReadLine();
        }
    }
}
