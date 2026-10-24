namespace lb5
{
    public enum StudentState
    {
        Registered, 
        Active,    
        Suspended,  
        Graduated, 
        Expelled   
    }

    public enum Trigger
    {
        PayTuition, 
        FailExam,   
        RetakeExam, 
        FailRetake, 
        PassThesis  
    }

    public class Student
    {
        public string Name { get; private set; }
        public StudentState CurrentState { get; private set; }

        public int AcademicDebts { get; set; } = 0;

        public Student(string name)
        {
            Name = name;
            CurrentState = StudentState.Registered;
            Console.WriteLine($"[Вхідний ефект] Створено профіль студента {Name}. Стан: {CurrentState}");
        }

        public void ApplyTrigger(Trigger trigger, decimal? paymentAmount = null)
        {
            Console.WriteLine($"\n-> Спроба застосувати подію: {trigger}");

            switch (CurrentState)
            {
                case StudentState.Registered:
                    if (trigger == Trigger.PayTuition)
                    {
                        if (paymentAmount.HasValue && paymentAmount.Value > 0)
                        {
                            TransitionTo(StudentState.Active);
                            Console.WriteLine("[Ефект] Оплату зараховано. Видано студентський квиток.");
                        }
                        else
                        {
                            Console.WriteLine("[Блокування] Захисна умова не виконана: Сума оплати має бути більше 0.");
                        }
                    }
                    break;

                case StudentState.Active:
                    if (trigger == Trigger.FailExam)
                    {
                        AcademicDebts++;
                        Console.WriteLine($"Отримано борг. Кількість боргів: {AcademicDebts}");

                        if (AcademicDebts >= 3)
                        {
                            TransitionTo(StudentState.Suspended);
                        }
                    }
                    else if (trigger == Trigger.PassThesis)
                    {
                        if (AcademicDebts == 0)
                        {
                            TransitionTo(StudentState.Graduated);
                            Console.WriteLine("[Вихідний ефект] Акаунт студента перенесено до архіву випускників.");
                        }
                        else
                        {
                            Console.WriteLine("[Блокування] Неможливо здати диплом, є академічна заборгованість.");
                        }
                    }
                    break;

                case StudentState.Suspended:
                    if (trigger == Trigger.RetakeExam)
                    {
                        TransitionTo(StudentState.Active);
                        AcademicDebts = 0; 
                        Console.WriteLine("[Ефект] Всі борги анульовано.");
                    }
                    else if (trigger == Trigger.FailRetake)
                    {
                        TransitionTo(StudentState.Expelled);
                        Console.WriteLine("[Вихідний ефект] Доступ до системи університету заблоковано.");
                    }
                    break;

                case StudentState.Graduated:
                case StudentState.Expelled:
                    Console.WriteLine("[Помилка] Студент у фінальному стані. Жодні дії неможливі.");
                    break;

                default:
                    Console.WriteLine("Невідома подія для поточного стану.");
                    break;
            }
        }

        private void TransitionTo(StudentState newState)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[ЗМІНА СТАНУ] {CurrentState} ---> {newState}");
            Console.ResetColor();
            CurrentState = newState;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Моделювання діаграми станів Університету ===\n");

            Student student = new Student("Безручко В. С.");

            student.ApplyTrigger(Trigger.PayTuition, 0m);

            student.ApplyTrigger(Trigger.PayTuition, 15000m);

            student.ApplyTrigger(Trigger.FailExam);
            student.ApplyTrigger(Trigger.FailExam);
            student.ApplyTrigger(Trigger.FailExam); 

            student.ApplyTrigger(Trigger.RetakeExam);

            student.ApplyTrigger(Trigger.PassThesis);

            student.ApplyTrigger(Trigger.FailExam);

            Console.ReadLine();
        }
    }
}
