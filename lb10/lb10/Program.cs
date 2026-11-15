using System;

namespace SolidRefactoringApp
{
    public interface INotificationSender
    {
        void Send(string message);
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public interface INotificationRepository
    {
        void Save(string message);
    }

    public class EmailSender : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending EMAIL: {message}");
        }
    }

    public class SmsSender : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
        }
    }

    public class PushSender : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending PUSH: {message}");
        }
    }

    public class TelegramSender : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending TELEGRAM: {message}");
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Logging: {message}");
        }
    }

    public class DatabaseNotificationRepository : INotificationRepository
    {
        public void Save(string message)
        {
            Console.WriteLine($"Saving to DB: {message}");
        }
    }

    public class NotificationManager
    {
        private readonly INotificationSender _sender;
        private readonly ILogger _logger;
        private readonly INotificationRepository _repository;

        public NotificationManager(
            INotificationSender sender,
            ILogger logger,
            INotificationRepository repository)
        {
            _sender = sender;
            _logger = logger;
            _repository = repository;
        }

        public void ProcessNotification(string message)
        {
            _sender.Send(message);
            _repository.Save(message);
            _logger.Log(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ILogger logger = new ConsoleLogger();
            INotificationRepository repository = new DatabaseNotificationRepository();

            INotificationSender emailSender = new EmailSender();
            NotificationManager emailManager = new NotificationManager(emailSender, logger, repository);
            emailManager.ProcessNotification("Вітаємо з успішною реєстрацією!");

            Console.WriteLine();

            INotificationSender telegramSender = new TelegramSender();
            NotificationManager telegramManager = new NotificationManager(telegramSender, logger, repository);
            telegramManager.ProcessNotification("Ваше замовлення відправлено!");

            Console.ReadLine();
        }
    }
}