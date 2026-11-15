using System;

namespace StructuralPatternsApp
{
    namespace Task1_Proxy
    {
        public interface IMedicalRecord
        {
            void Display();
        }

        public class RealMedicalRecord : IMedicalRecord
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Diagnosis { get; set; }
            public string TreatmentHistory { get; set; }

            public void Display()
            {
                Console.WriteLine($"Ім'я: {Name}, Вік: {Age}");
                Console.WriteLine($"Діагноз: {Diagnosis}");
                Console.WriteLine($"Історія лікування: {TreatmentHistory}");
            }
        }

        public class MedicalRecordProxy : IMedicalRecord
        {
            private readonly RealMedicalRecord _realRecord;
            private readonly string _userRole;

            public MedicalRecordProxy(RealMedicalRecord realRecord, string userRole)
            {
                _realRecord = realRecord;
                _userRole = userRole;
            }

            public void Display()
            {
                if (_userRole.ToLower() == "doctor")
                {
                    _realRecord.Display();
                }
                else if (_userRole.ToLower() == "admin")
                {
                    Console.WriteLine($"Ім'я: {_realRecord.Name}, Вік: {_realRecord.Age}");
                }
                else
                {
                    Console.WriteLine("Доступ заборонено.");
                }
            }
        }
    }

    namespace Task2_Decorator
    {
        public abstract class Subscription
        {
            public abstract string GetDescription();
            public abstract double GetCost();
        }

        public class BasicSubscription : Subscription
        {
            public override string GetDescription()
            {
                return "Базова підписка";
            }

            public override double GetCost()
            {
                return 150.00;
            }
        }

        public class HdDecorator : Subscription
        {
            private readonly Subscription _subscription;

            public HdDecorator(Subscription subscription)
            {
                _subscription = subscription;
            }

            public override string GetDescription()
            {
                return _subscription.GetDescription() + ", HD-якість";
            }

            public override double GetCost()
            {
                return _subscription.GetCost() + 50.00;
            }
        }

        public class OfflineDecorator : Subscription
        {
            private readonly Subscription _subscription;

            public OfflineDecorator(Subscription subscription)
            {
                _subscription = subscription;
            }

            public override string GetDescription()
            {
                return _subscription.GetDescription() + ", Перегляд офлайн";
            }

            public override double GetCost()
            {
                return _subscription.GetCost() + 70.00;
            }
        }

        public class AdFreeDecorator : Subscription
        {
            private readonly Subscription _subscription;

            public AdFreeDecorator(Subscription subscription)
            {
                _subscription = subscription;
            }

            public override string GetDescription()
            {
                return _subscription.GetDescription() + ", Без реклами";
            }

            public override double GetCost()
            {
                return _subscription.GetCost() + 100.00;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Завдання 1: Патерн Proxy (Замісник) ===");

            var realRecord = new Task1_Proxy.RealMedicalRecord
            {
                Name = "Петренко Петро",
                Age = 35,
                Diagnosis = "Мігрень",
                TreatmentHistory = "Призначено знеболювальні"
            };

            Console.WriteLine("--- Доступ Адміністратора ---");
            Task1_Proxy.IMedicalRecord adminProxy = new Task1_Proxy.MedicalRecordProxy(realRecord, "admin");
            adminProxy.Display();

            Console.WriteLine("\n--- Доступ Лікаря ---");
            Task1_Proxy.IMedicalRecord doctorProxy = new Task1_Proxy.MedicalRecordProxy(realRecord, "doctor");
            doctorProxy.Display();


            Console.WriteLine("\n=== Завдання 2: Патерн Decorator (Декоратор) ===");

            Task2_Decorator.Subscription mySub = new Task2_Decorator.BasicSubscription();
            Console.WriteLine($"{mySub.GetDescription()} | Вартість: {mySub.GetCost()} грн");

            mySub = new Task2_Decorator.HdDecorator(mySub);
            Console.WriteLine($"{mySub.GetDescription()} | Вартість: {mySub.GetCost()} грн");

            mySub = new Task2_Decorator.OfflineDecorator(mySub);
            Console.WriteLine($"{mySub.GetDescription()} | Вартість: {mySub.GetCost()} грн");

            mySub = new Task2_Decorator.AdFreeDecorator(mySub);
            Console.WriteLine($"{mySub.GetDescription()} | Вартість: {mySub.GetCost()} грн");

            Console.ReadLine();
        }
    }
}