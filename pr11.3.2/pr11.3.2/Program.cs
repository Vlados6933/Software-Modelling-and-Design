using System;

namespace CompanyTemplateMethodApp
{
    public abstract class ReportGenerator
    {
        public void GenerateReport()
        {
            GatherData();
            ProcessData();
            FormatReport();
            PrintReport();
        }

        protected abstract void GatherData();
        protected abstract void ProcessData();
        protected abstract void FormatReport();

        private void PrintReport()
        {
            Console.WriteLine("Звіт успішно згенеровано та виведено на друк.\n");
        }
    }

    public class FinancialReportGenerator : ReportGenerator
    {
        protected override void GatherData()
        {
            Console.WriteLine("Збір фінансових даних з бази ERP-системи...");
        }

        protected override void ProcessData()
        {
            Console.WriteLine("Розрахунок чистого прибутку, витрат та податків...");
        }

        protected override void FormatReport()
        {
            Console.WriteLine("Форматування фінансового звіту у вигляді таблиці Excel...");
        }
    }

    public class HrReportGenerator : ReportGenerator
    {
        protected override void GatherData()
        {
            Console.WriteLine("Збір даних про співробітників з HR-бази...");
        }

        protected override void ProcessData()
        {
            Console.WriteLine("Аналіз відпрацьованих годин, відпусток та лікарняних...");
        }

        protected override void FormatReport()
        {
            Console.WriteLine("Форматування кадрового звіту у форматі PDF-документа...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Генерація Фінансового Звіту ===");
            ReportGenerator financialReport = new FinancialReportGenerator();
            financialReport.GenerateReport();

            Console.WriteLine("=== Генерація Кадрового Звіту (HR) ===");
            ReportGenerator hrReport = new HrReportGenerator();
            hrReport.GenerateReport();

            Console.ReadLine();
        }
    }
}