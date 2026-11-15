using System;

namespace CompanyBehavioralApp
{
    public class ExpenseReport
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
    }

    public interface IApprover
    {
        IApprover SetNext(IApprover approver);
        void HandleRequest(ExpenseReport request);
    }

    public abstract class ApproverBase : IApprover
    {
        private IApprover _nextApprover;

        public IApprover SetNext(IApprover approver)
        {
            _nextApprover = approver;
            return approver;
        }

        public virtual void HandleRequest(ExpenseReport request)
        {
            if (_nextApprover != null)
            {
                _nextApprover.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"Відмова: Запит #{request.Id} на {request.Amount} грн не може бути оброблений.");
            }
        }
    }

    public class TeamLead : ApproverBase
    {
        public override void HandleRequest(ExpenseReport request)
        {
            if (request.Amount <= 5000)
            {
                Console.WriteLine($"TeamLead погодив запит #{request.Id} на {request.Amount} грн ({request.Purpose}).");
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }

    public class Manager : ApproverBase
    {
        public override void HandleRequest(ExpenseReport request)
        {
            if (request.Amount <= 15000)
            {
                Console.WriteLine($"Manager погодив запит #{request.Id} на {request.Amount} грн ({request.Purpose}).");
            }
            else
            {
                base.HandleRequest(request);
            }
        }
    }

    public class Director : ApproverBase
    {
        public override void HandleRequest(ExpenseReport request)
        {
            Console.WriteLine($"Director погодив запит #{request.Id} на {request.Amount} грн ({request.Purpose}).");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var teamLead = new TeamLead();
            var manager = new Manager();
            var director = new Director();

            teamLead.SetNext(manager).SetNext(director);

            var request1 = new ExpenseReport { Id = 1, Amount = 3000, Purpose = "Канцтовари" };
            var request2 = new ExpenseReport { Id = 2, Amount = 12000, Purpose = "Оновлення ПЗ" };
            var request3 = new ExpenseReport { Id = 3, Amount = 45000, Purpose = "Корпоративне обладнання" };

            Console.WriteLine("=== Обробка запитів системою компанії ===");
            teamLead.HandleRequest(request1);
            teamLead.HandleRequest(request2);
            teamLead.HandleRequest(request3);

            Console.ReadLine();
        }
    }
}