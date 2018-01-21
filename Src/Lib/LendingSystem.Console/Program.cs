using System;
using System.Globalization;
using System.IO;
using System.Threading;
using LendingSystem.Data;
using LendingSystem.Extensions;
using LendingSystem.Models;

namespace LendingSystem.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

            if (!ValidateParameters(args, out string csvPath, out decimal amount))
            {
                return;
            }

            var csvLendersDal = new CsvLendersDal(csvPath);
            var lendingSystem = new LendingSystem(csvLendersDal);

            ILoan loan;
            try
            {
                if (!lendingSystem.HasEnoughResources(amount))
                {
                    System.Console.Error.WriteLine("Not enough resources to lend!");
                    return;
                }

                loan = lendingSystem.Lend(amount);
            }
            catch (Exception ex)
            {
                System.Console.Error.WriteLine($"Unexpected error occured during calculation: {ex.Message}");
                return;
            }

            System.Console.WriteLine($"Requested amount: £{amount.Round()}");
            System.Console.WriteLine($"Rate: {loan.AnnualInterestRate:P1}");
            System.Console.WriteLine($"Monthly repayment: £{loan.PaymentAmountPerPeriod.Round()}");
            System.Console.WriteLine($"Total repayment: £{loan.TotalPaymentAmount.Round()}");
        }

        private static bool ValidateParameters(string[] args, out string csvPath, out decimal amount)
        {
            csvPath = String.Empty;
            amount = 0;

            if (args.Length != 2)
            {
                System.Console.Error.WriteLine("Incorrect number of parameters! Use paramters like [market_file] [loan_amount]");
                return false;
            }

            string filePath = args[0];
            csvPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), filePath);
            if (!File.Exists(csvPath))
            {
                System.Console.Error.WriteLine($"File {filePath} does not exist!");
                return false;
            }

            if (!decimal.TryParse(args[1], out amount))
            {
                System.Console.Error.WriteLine($"Amount {args[2]} is not correct!");
                return false;
            }

            if (amount < 100 || amount > 15000)
            {
                System.Console.Error.WriteLine("Please enter values between 100 and 15000 inclusive.");
                return false;
            }

            if (amount % 100 != 0)
            {
                System.Console.Error.WriteLine("Entered value must be an increment of 100.");
                return false;
            }
            return true;
        }
    }
}
