using System;
using System.Collections.Generic;

namespace DesignPrinciplesApp
{
    namespace Task1_SoC
    {
        public interface IOrderRepository
        {
            void Save(string item);
        }

        public interface IEmailService
        {
            void SendConfirmation();
        }

        public class OrderRepository : IOrderRepository
        {
            public void Save(string item)
            {
                Console.WriteLine($"Saving order '{item}' to DB");
            }
        }

        public class EmailService : IEmailService
        {
            public void SendConfirmation()
            {
                Console.WriteLine("Sending email confirmation");
            }
        }

        public class OrderProcessor
        {
            private readonly IOrderRepository _repository;
            private readonly IEmailService _emailService;

            public OrderProcessor(IOrderRepository repository, IEmailService emailService)
            {
                _repository = repository;
                _emailService = emailService;
            }

            public void ProcessOrder(string item)
            {
                _repository.Save(item);
                _emailService.SendConfirmation();
            }
        }
    }

    namespace Task2_Modularity
    {
        public class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

        public interface IBookRepository
        {
            void Add(Book book);
            List<Book> GetAll();
        }

        public class BookRepository : IBookRepository
        {
            private readonly List<Book> _books = new List<Book>();

            public void Add(Book book)
            {
                _books.Add(book);
            }

            public List<Book> GetAll()
            {
                return _books;
            }
        }

        public class LibraryService
        {
            private readonly IBookRepository _repository;

            public LibraryService(IBookRepository repository)
            {
                _repository = repository;
            }

            public void RegisterBook(Book book)
            {
                _repository.Add(book);
            }

            public List<Book> GetAvailableBooks()
            {
                return _repository.GetAll();
            }
        }

        public class ConsoleUI
        {
            private readonly LibraryService _service;

            public ConsoleUI(LibraryService service)
            {
                _service = service;
            }

            public void Run()
            {
                _service.RegisterBook(new Book { Id = 1, Title = "C# Architecture" });
                _service.RegisterBook(new Book { Id = 2, Title = "Design Patterns" });

                var books = _service.GetAvailableBooks();
                foreach (var b in books)
                {
                    Console.WriteLine($"Book: {b.Title}");
                }
            }
        }
    }

    namespace Task3_4_AbstractionAndHiding
    {
        public abstract class Vehicle
        {
            private bool _isRunning;

            public void Start()
            {
                _isRunning = true;
                OnStart();
            }

            public void Stop()
            {
                _isRunning = false;
                OnStop();
            }

            public bool IsRunning()
            {
                return _isRunning;
            }

            protected abstract void OnStart();
            protected abstract void OnStop();
        }

        public class Car : Vehicle
        {
            private int _fuelLevel = 100;

            protected override void OnStart()
            {
                if (_fuelLevel > 0)
                {
                    Console.WriteLine("Car engine started.");
                    _fuelLevel -= 10;
                }
                else
                {
                    Console.WriteLine("Cannot start: Out of fuel.");
                }
            }

            protected override void OnStop()
            {
                Console.WriteLine("Car engine stopped.");
            }
        }

        public class Bicycle : Vehicle
        {
            private bool _isChainLubricated = true;

            protected override void OnStart()
            {
                if (_isChainLubricated)
                {
                    Console.WriteLine("Bicycle is moving.");
                }
            }

            protected override void OnStop()
            {
                Console.WriteLine("Bicycle stopped.");
            }
        }
    }

    namespace Task5_TDD
    {
        public class Calculator
        {
            public int Add(int a, int b)
            {
                return a + b;
            }
        }

        public class CalculatorTests
        {
            public void Add_TwoNumbers_ReturnsSum()
            {
                var calculator = new Calculator();
                int result = calculator.Add(5, 7);

                if (result == 12)
                {
                    Console.WriteLine("Test Passed: 5 + 7 = 12");
                }
                else
                {
                    throw new Exception("Test Failed.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Task 1: SoC ===");
            var orderProcessor = new Task1_SoC.OrderProcessor(
                new Task1_SoC.OrderRepository(),
                new Task1_SoC.EmailService());
            orderProcessor.ProcessOrder("Laptop");

            Console.WriteLine("\n=== Task 2: Modularity ===");
            var ui = new Task2_Modularity.ConsoleUI(
                new Task2_Modularity.LibraryService(
                    new Task2_Modularity.BookRepository()));
            ui.Run();

            Console.WriteLine("\n=== Task 3 & 4: Abstraction & Information Hiding ===");
            Task3_4_AbstractionAndHiding.Vehicle car = new Task3_4_AbstractionAndHiding.Car();
            car.Start();
            Console.WriteLine($"Is vehicle running? {car.IsRunning()}");
            car.Stop();

            Console.WriteLine("\n=== Task 5: TDD ===");
            var testRunner = new Task5_TDD.CalculatorTests();
            testRunner.Add_TwoNumbers_ReturnsSum();

            Console.ReadLine();
        }
    }
}