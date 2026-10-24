using System;
using System.Threading.Tasks;

namespace CompanyActivityApp
{
    public class CompanyWorkflow
    {
        public event Action<string> OrderReceived;
        public event Action<string> PaymentProcessed;
        public event Action<string> GoodsPrepared;
        public event Action<string> OrderShipped;

        public CompanyWorkflow()
        {
            OrderReceived += order => Console.WriteLine($"[ПОДІЯ ОТРИМАНА] Замовлення '{order}' прийнято в роботу.");
            PaymentProcessed += order => Console.WriteLine($"[ПОДІЯ ОТРИМАНА] Оплата за '{order}' підтверджена.");
            GoodsPrepared += order => Console.WriteLine($"[ПОДІЯ ОТРИМАНА] Товар для '{order}' зібрано на складі.");
            OrderShipped += order => Console.WriteLine($"[ПОДІЯ ОТРИМАНА] Замовлення '{order}' відправлено клієнту.");
        }

        public async Task ExecuteWorkflowAsync(string orderName)
        {
            Console.WriteLine("--- Старт бізнес-процесу ---");
            OrderReceived?.Invoke(orderName);

            var paymentTask = ProcessPaymentAsync(orderName);
            var preparationTask = PrepareGoodsAsync(orderName);

            await Task.WhenAll(paymentTask, preparationTask);

            OrderShipped?.Invoke(orderName);
            Console.WriteLine("--- Бізнес-процес завершено ---");
        }

        private async Task ProcessPaymentAsync(string orderName)
        {
            await Task.Delay(1000);
            PaymentProcessed?.Invoke(orderName);
        }

        private async Task PrepareGoodsAsync(string orderName)
        {
            await Task.Delay(1500);
            GoodsPrepared?.Invoke(orderName);
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var workflow = new CompanyWorkflow();
            await workflow.ExecuteWorkflowAsync("Корпоративні ліцензії Office 365");

            Console.ReadLine();
        }
    }
}