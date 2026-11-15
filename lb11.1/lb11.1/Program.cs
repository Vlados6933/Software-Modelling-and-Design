using System;
using System.Collections.Generic;

namespace CreationalPatternsApp
{
    namespace Task1_Builder
    {
        public class Sandwich
        {
            public List<string> Ingredients { get; } = new List<string>();

            public void ShowIngredients()
            {
                Console.WriteLine("Склад сендвіча: " + string.Join(", ", Ingredients));
            }
        }

        public interface ISandwichBuilder
        {
            void AddBread();
            void AddMeat();
            void AddVegetables();
            void AddSauce();
            Sandwich GetSandwich();
        }

        public class VeggieSandwichBuilder : ISandwichBuilder
        {
            private Sandwich _sandwich = new Sandwich();

            public void AddBread()
            {
                _sandwich.Ingredients.Add("Цільнозерновий хліб");
            }

            public void AddMeat()
            {
            }

            public void AddVegetables()
            {
                _sandwich.Ingredients.Add("Салат, Помідор, Огірок, Оливки");
            }

            public void AddSauce()
            {
                _sandwich.Ingredients.Add("Веганський майонез");
            }

            public Sandwich GetSandwich()
            {
                return _sandwich;
            }
        }

        public class MeatLoversBuilder : ISandwichBuilder
        {
            private Sandwich _sandwich = new Sandwich();

            public void AddBread()
            {
                _sandwich.Ingredients.Add("Білий хліб");
            }

            public void AddMeat()
            {
                _sandwich.Ingredients.Add("Бекон, Шинка, Салямі, Індичка");
            }

            public void AddVegetables()
            {
                _sandwich.Ingredients.Add("Цибуля");
            }

            public void AddSauce()
            {
                _sandwich.Ingredients.Add("Соус Барбекю");
            }

            public Sandwich GetSandwich()
            {
                return _sandwich;
            }
        }

        public class Director
        {
            private ISandwichBuilder _builder;

            public Director(ISandwichBuilder builder)
            {
                _builder = builder;
            }

            public void Construct()
            {
                _builder.AddBread();
                _builder.AddMeat();
                _builder.AddVegetables();
                _builder.AddSauce();
            }
        }
    }

    namespace Task2_Singleton
    {
        public class ConfigurationManager
        {
            private static readonly Lazy<ConfigurationManager> _instance =
                new Lazy<ConfigurationManager>(() => new ConfigurationManager());

            private readonly Dictionary<string, string> _settings;

            private ConfigurationManager()
            {
                _settings = new Dictionary<string, string>
                {
                    { "AppVersion", "1.0.0" },
                    { "Theme", "Dark" },
                    { "DbConnectionString", "Server=localhost;Database=UniDB;" }
                };
            }

            public static ConfigurationManager Instance => _instance.Value;

            public string GetSetting(string key)
            {
                if (_settings.ContainsKey(key))
                {
                    return _settings[key];
                }
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Завдання 1: Патерн Builder ===");

            var veggieBuilder = new Task1_Builder.VeggieSandwichBuilder();
            var director1 = new Task1_Builder.Director(veggieBuilder);
            director1.Construct();
            var veggieSandwich = veggieBuilder.GetSandwich();
            Console.WriteLine("Вегетаріанський сендвіч:");
            veggieSandwich.ShowIngredients();

            var meatBuilder = new Task1_Builder.MeatLoversBuilder();
            var director2 = new Task1_Builder.Director(meatBuilder);
            director2.Construct();
            var meatSandwich = meatBuilder.GetSandwich();
            Console.WriteLine("\nМ'ясний сендвіч:");
            meatSandwich.ShowIngredients();


            Console.WriteLine("\n=== Завдання 2: Патерн Singleton ===");

            var config1 = Task2_Singleton.ConfigurationManager.Instance;
            var config2 = Task2_Singleton.ConfigurationManager.Instance;

            Console.WriteLine($"Хеш-код об'єкта config1: {config1.GetHashCode()}");
            Console.WriteLine($"Хеш-код об'єкта config2: {config2.GetHashCode()}");
            Console.WriteLine($"Чи є config1 та config2 одним і тим самим екземпляром? {ReferenceEquals(config1, config2)}");

            Console.WriteLine($"\nОтримання налаштування 'Theme' з config1: {config1.GetSetting("Theme")}");
            Console.WriteLine($"Отримання налаштування 'AppVersion' з config2: {config2.GetSetting("AppVersion")}");

            Console.ReadLine();
        }
    }
}