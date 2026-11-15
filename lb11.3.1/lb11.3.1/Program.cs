using System;
using System.Collections.Generic;

namespace CommandPatternUniversity
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class UniversityDatabase
    {
        private readonly List<string> _grades = new List<string>();

        public void AddGrade(string student, int grade)
        {
            string record = $"{student}: {grade}";
            _grades.Add(record);
            Console.WriteLine($"[БД] Додано оцінку: {record}");
        }

        public void RemoveGrade(string student, int grade)
        {
            string record = $"{student}: {grade}";
            if (_grades.Contains(record))
            {
                _grades.Remove(record);
                Console.WriteLine($"[БД] Видалено оцінку: {record}");
            }
        }

        public void PrintGrades()
        {
            Console.WriteLine("--- Поточна відомість ---");
            if (_grades.Count == 0)
            {
                Console.WriteLine("Пусто");
            }
            else
            {
                foreach (var g in _grades)
                {
                    Console.WriteLine(g);
                }
            }
            Console.WriteLine("-------------------------");
        }
    }

    public class AddGradeCommand : ICommand
    {
        private readonly UniversityDatabase _database;
        private readonly string _student;
        private readonly int _grade;

        public AddGradeCommand(UniversityDatabase database, string student, int grade)
        {
            _database = database;
            _student = student;
            _grade = grade;
        }

        public void Execute()
        {
            _database.AddGrade(_student, _grade);
        }

        public void Undo()
        {
            _database.RemoveGrade(_student, _grade);
        }
    }

    public class DeanerySystem
    {
        private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public void ExecuteOperation(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command);
        }

        public void UndoLastOperation()
        {
            if (_commandHistory.Count > 0)
            {
                Console.WriteLine("\n[Деканат] Скасування останньої операції...");
                ICommand lastCommand = _commandHistory.Pop();
                lastCommand.Undo();
            }
            else
            {
                Console.WriteLine("\n[Деканат] Немає операцій для скасування.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var database = new UniversityDatabase();
            var deanery = new DeanerySystem();

            ICommand addGrade1 = new AddGradeCommand(database, "Безручко В. С.", 95);
            ICommand addGrade2 = new AddGradeCommand(database, "Петренко П. П.", 82);
            ICommand addGrade3 = new AddGradeCommand(database, "Іванов І. І.", 60);

            deanery.ExecuteOperation(addGrade1);
            deanery.ExecuteOperation(addGrade2);
            deanery.ExecuteOperation(addGrade3);

            database.PrintGrades();

            deanery.UndoLastOperation();
            database.PrintGrades();

            deanery.UndoLastOperation();
            database.PrintGrades();

            Console.ReadLine();
        }
    }
}