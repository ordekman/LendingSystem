using System;
using LendingSystem.Extensions;
using LendingSystem.Models;
using NUnit.Framework;

namespace LendingSystem.Tests
{
    [TestFixture]
    public class LoanTest
    {
        [TestCase(1000.00, 0.1, (uint) 1, (uint) 1, ExpectedResult = 1100.00)]
        [TestCase(1000.00, 0.0, (uint)1, (uint)1, ExpectedResult = 1000.00)]
        [TestCase(1000.00, -0.1, (uint)1, (uint)1, ExpectedResult = 900.00)]
        [TestCase(1000.00, 0.07, (uint)36, (uint)12, ExpectedResult = 1108.04)]
        public decimal Constructor_ValidArguments_TotalPaymentAmountIsComputedCorrectly(decimal amount, decimal annualInterestRate, uint periodsNumber,
            uint periodsPerYear)
        {
            Loan loan = new Loan(amount, annualInterestRate, periodsNumber, periodsPerYear);
            return loan.TotalPaymentAmount.Round();
        }

        [TestCase(1000.00, 0.1, (uint)1, (uint)1, ExpectedResult = 1100.00)]
        [TestCase(1000.00, 0.0, (uint)1, (uint)1, ExpectedResult = 1000.00)]
        [TestCase(1200.00, 0.0, (uint)12, (uint)12, ExpectedResult = 100.00)]
        [TestCase(1000.00, -0.1, (uint)1, (uint)1, ExpectedResult = 900.00)]
        [TestCase(1000.00, 0.07, (uint)36, (uint)12, ExpectedResult = 30.78)]
        public decimal Constructor_ValidArguments_PaymentAmountPerPeriodIsComputedCorrectly(decimal amount, decimal annualInterestRate, uint periodsNumber,
            uint periodsPerYear)
        {
            Loan loan = new Loan(amount, annualInterestRate, periodsNumber, periodsPerYear);
            return loan.PaymentAmountPerPeriod.Round();
        }

        [Test]
        public void Constructor_AmountIsZero_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Loan(0, 1, 1, 1));
        }
    }
}
