using System;
using System.Collections.Generic;

namespace StructuralPatternsApp
{
    namespace Task1_Adapter
    {
        public interface IDistanceTracker
        {
            double GetDistanceKm();
        }

        public class ExternalTracker
        {
            public double GetDistanceMiles()
            {
                return 5.5;
            }
        }

        public class DistanceAdapter : IDistanceTracker
        {
            private readonly ExternalTracker _externalTracker;
            private const double MilesToKmFactor = 1.60934;

            public DistanceAdapter(ExternalTracker externalTracker)
            {
                _externalTracker = externalTracker;
            }

            public double GetDistanceKm()
            {
                double miles = _externalTracker.GetDistanceMiles();
                return Math.Round(miles * MilesToKmFactor, 2);
            }
        }
    }

    namespace Task2_Composite
    {
        public abstract class CourseComponent
        {
            protected string _name;

            public CourseComponent(string name)
            {
                _name = name;
            }

            public abstract void Display(int depth = 0);

            public virtual void Add(CourseComponent component)
            {
                throw new NotSupportedException("Даний компонент не підтримує додавання вкладених елементів.");
            }
        }

        public class Lesson : CourseComponent
        {
            public Lesson(string name) : base(name)
            {
            }

            public override void Display(int depth = 0)
            {
                Console.WriteLine(new string('-', depth) + " Урок: " + _name);
            }
        }

        public class Module : CourseComponent
        {
            private readonly List<CourseComponent> _components = new List<CourseComponent>();

            public Module(string name) : base(name)
            {
            }

            public override void Add(CourseComponent component)
            {
                _components.Add(component);
            }

            public override void Display(int depth = 0)
            {
                Console.WriteLine(new string('-', depth) + " Модуль: " + _name);

                foreach (var component in _components)
                {
                    component.Display(depth + 2);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Завдання 1: Патерн Adapter ===");

            Task1_Adapter.ExternalTracker thirdPartyTracker = new Task1_Adapter.ExternalTracker();
            Task1_Adapter.IDistanceTracker fitnessAppTracker = new Task1_Adapter.DistanceAdapter(thirdPartyTracker);

            Console.WriteLine($"Дані зі стороннього трекера (в милях): {thirdPartyTracker.GetDistanceMiles()}");
            Console.WriteLine($"Адаптовані дані для нашої системи (в кілометрах): {fitnessAppTracker.GetDistanceKm()}");


            Console.WriteLine("\n=== Завдання 2: Патерн Composite ===");

            Task2_Composite.CourseComponent mainCourse = new Task2_Composite.Module("Основи C#");

            Task2_Composite.CourseComponent module1 = new Task2_Composite.Module("Вступ");
            module1.Add(new Task2_Composite.Lesson("Синтаксис мови"));
            module1.Add(new Task2_Composite.Lesson("Змінні та типи даних"));

            Task2_Composite.CourseComponent module2 = new Task2_Composite.Module("Об'єктно-орієнтоване програмування");
            module2.Add(new Task2_Composite.Lesson("Класи та об'єкти"));

            Task2_Composite.CourseComponent subModule2_1 = new Task2_Composite.Module("Принципи ООП");
            subModule2_1.Add(new Task2_Composite.Lesson("Інкапсуляція"));
            subModule2_1.Add(new Task2_Composite.Lesson("Поліморфізм"));

            module2.Add(subModule2_1);

            mainCourse.Add(module1);
            mainCourse.Add(module2);

            mainCourse.Display();

            Console.ReadLine();
        }
    }
}