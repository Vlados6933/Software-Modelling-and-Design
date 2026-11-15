using System;

namespace PatternsApp
{
    namespace Task1_FactoryMethod
    {
        public abstract class Course
        {
            public abstract void StartCourse();
        }

        public class OnlineCourse : Course
        {
            public override void StartCourse()
            {
                Console.WriteLine("Online course started: Joining via Zoom.");
            }
        }

        public class OfflineCourse : Course
        {
            public override void StartCourse()
            {
                Console.WriteLine("Offline course started: Please proceed to the classroom.");
            }
        }

        public abstract class CourseFactory
        {
            public abstract Course CreateCourse(string courseType);
        }

        public class ConcreteCourseFactory : CourseFactory
        {
            public override Course CreateCourse(string courseType)
            {
                switch (courseType.ToLower())
                {
                    case "online":
                        return new OnlineCourse();
                    case "offline":
                        return new OfflineCourse();
                    default:
                        throw new ArgumentException("Unknown course type.");
                }
            }
        }
    }

    namespace Task2_AbstractFactory
    {
        public interface IButton
        {
            void Render();
            void OnClick();
        }

        public interface ICheckbox
        {
            void Render();
            void OnClick();
        }

        public class WindowsButton : IButton
        {
            public void Render()
            {
                Console.WriteLine("Rendering Windows Style Button.");
            }

            public void OnClick()
            {
                Console.WriteLine("Windows Button clicked.");
            }
        }

        public class WindowsCheckbox : ICheckbox
        {
            public void Render()
            {
                Console.WriteLine("Rendering Windows Style Checkbox.");
            }

            public void OnClick()
            {
                Console.WriteLine("Windows Checkbox state toggled.");
            }
        }

        public class MacButton : IButton
        {
            public void Render()
            {
                Console.WriteLine("Rendering Mac OS Style Button.");
            }

            public void OnClick()
            {
                Console.WriteLine("Mac Button clicked.");
            }
        }

        public class MacCheckbox : ICheckbox
        {
            public void Render()
            {
                Console.WriteLine("Rendering Mac OS Style Checkbox.");
            }

            public void OnClick()
            {
                Console.WriteLine("Mac Checkbox state toggled.");
            }
        }

        public interface IUIFactory
        {
            IButton CreateButton();
            ICheckbox CreateCheckbox();
        }

        public class WindowsFactory : IUIFactory
        {
            public IButton CreateButton()
            {
                return new WindowsButton();
            }

            public ICheckbox CreateCheckbox()
            {
                return new WindowsCheckbox();
            }
        }

        public class MacFactory : IUIFactory
        {
            public IButton CreateButton()
            {
                return new MacButton();
            }

            public ICheckbox CreateCheckbox()
            {
                return new MacCheckbox();
            }
        }

        public class Application
        {
            private readonly IButton _button;
            private readonly ICheckbox _checkbox;

            public Application(IUIFactory factory)
            {
                _button = factory.CreateButton();
                _checkbox = factory.CreateCheckbox();
            }

            public void Paint()
            {
                _button.Render();
                _checkbox.Render();
            }

            public void TriggerEvents()
            {
                _button.OnClick();
                _checkbox.OnClick();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Task 1: Factory Method ===");

            Task1_FactoryMethod.CourseFactory courseFactory = new Task1_FactoryMethod.ConcreteCourseFactory();

            Task1_FactoryMethod.Course onlineCourse = courseFactory.CreateCourse("online");
            onlineCourse.StartCourse();

            Task1_FactoryMethod.Course offlineCourse = courseFactory.CreateCourse("offline");
            offlineCourse.StartCourse();

            Console.WriteLine("\n=== Task 2: Abstract Factory ===");

            Console.WriteLine("--> Launching App on Windows Environment:");
            Task2_AbstractFactory.IUIFactory winFactory = new Task2_AbstractFactory.WindowsFactory();
            Task2_AbstractFactory.Application winApp = new Task2_AbstractFactory.Application(winFactory);
            winApp.Paint();
            winApp.TriggerEvents();

            Console.WriteLine("\n--> Launching App on Mac OS Environment:");
            Task2_AbstractFactory.IUIFactory macFactory = new Task2_AbstractFactory.MacFactory();
            Task2_AbstractFactory.Application macApp = new Task2_AbstractFactory.Application(macFactory);
            macApp.Paint();
            macApp.TriggerEvents();

            Console.ReadLine();
        }
    }
}