using System;
using System.Threading;

namespace UniversityDeploymentApp
{
    public class UniversityDatabase
    {
        public string FetchStudentData(int studentId)
        {
            Thread.Sleep(300);
            return $"Student_{studentId}_Data_From_DB";
        }
    }

    public class ApplicationServer
    {
        private UniversityDatabase _database = new UniversityDatabase();

        public string ProcessStudentRequest(int studentId)
        {
            Console.WriteLine($"[Сервер додатків] Обробка бізнес-логіки для студента ID: {studentId}");
            string rawData = _database.FetchStudentData(studentId);
            return $"Processed_{rawData}";
        }
    }

    public class WebServer
    {
        private ApplicationServer _appServer = new ApplicationServer();

        public void HandleHttpRequest(int studentId)
        {
            Console.WriteLine($"[Веб-сервер] Отримано HTTP GET запит для студента {studentId}");

            string jsonResponse = _appServer.ProcessStudentRequest(studentId);

            Console.WriteLine($"[Веб-сервер] Відправка HTTP 200 OK на клієнт. Payload: {jsonResponse}");
        }
    }

    public class ClientDevice
    {
        private WebServer _webServer = new WebServer();

        public void ClickViewStudentProfile(int studentId)
        {
            Console.WriteLine($"[Клієнт-Браузер] Користувач натиснув 'Переглянути профіль' (ID: {studentId})");
            _webServer.HandleHttpRequest(studentId);
            Console.WriteLine($"[Клієнт-Браузер] Сторінку успішно відрендерено на екрані.\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Симуляція розподіленої архітектури університету ===\n");

            ClientDevice laptop = new ClientDevice();

            laptop.ClickViewStudentProfile(1045);
            laptop.ClickViewStudentProfile(2088);

            Console.ReadLine();
        }
    }
}