using System;

namespace StrategyPatternUniversity
{
    public interface ITuitionStrategy
    {
        decimal CalculateFee(decimal baseAmount);
    }

    public class StateFundedStrategy : ITuitionStrategy
    {
        public decimal CalculateFee(decimal baseAmount)
        {
            return 0m;
        }
    }

    public class ContractStrategy : ITuitionStrategy
    {
        public decimal CalculateFee(decimal baseAmount)
        {
            return baseAmount;
        }
    }

    public class DiscountStrategy : ITuitionStrategy
    {
        public decimal CalculateFee(decimal baseAmount)
        {
            return baseAmount * 0.8m;
        }
    }

    public class StudentBilling
    {
        private ITuitionStrategy _strategy;
        private readonly string _studentName;

        public StudentBilling(string studentName, ITuitionStrategy strategy)
        {
            _studentName = studentName;
            _strategy = strategy;
        }

        public void SetStrategy(ITuitionStrategy strategy)
        {
            _strategy = strategy;
        }

        public void GenerateInvoice(decimal baseAmount)
        {
            decimal finalAmount = _strategy.CalculateFee(baseAmount);
            Console.WriteLine($"Студент: {_studentName}");
            Console.WriteLine($"Застосована стратегія: {_strategy.GetType().Name}");
            Console.WriteLine($"До сплати: {finalAmount} грн\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            decimal baseTuition = 35000m;

            var billing = new StudentBilling("Безручко В. С.", new ContractStrategy());
            billing.GenerateInvoice(baseTuition);

            billing.SetStrategy(new DiscountStrategy());
            billing.GenerateInvoice(baseTuition);

            billing.SetStrategy(new StateFundedStrategy());
            billing.GenerateInvoice(baseTuition);

            Console.ReadLine();
        }
    }
}