namespace lb3
{
    public class Teacher
    {
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }

        public Teacher(string name, bool isAuthenticated)
        {
            Name = name;
            IsAuthenticated = isAuthenticated;
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int? Grade { get; set; }
    }

    public class SubmitGradeUseCase
    {
        public void Execute(Teacher teacher, Student student, int gradeValue)
        {
            Console.WriteLine($"\n--- Запуск прецеденту: Виставлення оцінки для {student.Name} ---");

            if (!teacher.IsAuthenticated)
            {
                Console.WriteLine("[Помилка] Викладач не авторизований в системі! Виконання перервано.");
                return;
            }

            Console.WriteLine("1. Система перевіряє права доступу викладача... [ОК]");

            if (gradeValue < 1 || gradeValue > 100)
            {
                Console.WriteLine($"[Помилка] Оцінка {gradeValue} є некоректною. Введіть значення від 1 до 100.");
                Console.WriteLine("2а. Система повідомляє про помилку формату.");
                return;
            }

            Console.WriteLine("2. Викладач вводить оцінку студенту.");
            student.Grade = gradeValue;

            Console.WriteLine("3. Система зберігає оцінку в базі даних.");
            Console.WriteLine($"[Успіх] Оцінку {student.Grade} для студента {student.Name} успішно збережено!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var teacher1 = new Teacher("Харицька С. В.", isAuthenticated: true);
            var teacher2 = new Teacher("Невідомий Викладач", isAuthenticated: false);
            var student = new Student { Name = "Безручко В. С." };

            var useCase = new SubmitGradeUseCase();

            useCase.Execute(teacher1, student, 95);

            useCase.Execute(teacher1, student, 150);

            useCase.Execute(teacher2, student, 80);

            Console.ReadLine();
        }
    }
}
