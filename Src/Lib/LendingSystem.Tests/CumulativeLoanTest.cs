using System;
using System.Collections.Generic;
using LendingSystem.Models;
using Moq;
using NUnit.Framework;

namespace LendingSystem.Tests
{
    [TestFixture]
    class CumulativeLoanTest
    {
        private CumulativeLoan _cumulativeLoan;

        [SetUp]
        public void SetUp()
        {
            _cumulativeLoan = PrepareCumulativeLoan();
        }

        [Test]
        public void PaymentAmountPerPeriod_ContainsValidLoans_AmountIsComputedCorrectly()
        {
            Assert.AreEqual(30, _cumulativeLoan.PaymentAmountPerPeriod);
        }

        [Test]
        public void TotalPaymentAmount_ContainsValidLoans_AmountIsComputedCorrectly()
        { 
            Assert.AreEqual(230, _cumulativeLoan.TotalPaymentAmount);
        }

        [Test]
        public void Amount_ContainsValidLoans_AmountIsComputedCorrectly()
        {
            Assert.AreEqual(200, _cumulativeLoan.Amount);
        }

        [Test]
        public void AnnualInterestRate_ContainsValidLoans_InterestRateIsComputedCorrectly()
        {
            Assert.AreEqual(10, _cumulativeLoan.AnnualInterestRate);
        }

        [Test]
        public void Constructor_LoandsAreNull_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new CumulativeLoan(null));
        }

        private CumulativeLoan PrepareCumulativeLoan()
        {
            Mock<ILoan> mock1 = PrepareLoanMock(10, 10, 100, 110);
            Mock<ILoan> mock2 = PrepareLoanMock(20, 10, 100, 120);

            var cumulativeLoan = new CumulativeLoan(new List<ILoan> {mock1.Object, mock2.Object});
            return cumulativeLoan;
        }

        private Mock<ILoan> PrepareLoanMock(decimal paymentAmountPerPeriod, decimal annualInterestRate, decimal amount, decimal totalPaymentAmount)
        {
            var loan = new Mock<ILoan>();
            loan.Setup(m => m.PaymentAmountPerPeriod).Returns(paymentAmountPerPeriod);
            loan.Setup(m => m.AnnualInterestRate).Returns(annualInterestRate);
            loan.Setup(m => m.Amount).Returns(amount);
            loan.Setup(m => m.TotalPaymentAmount).Returns(totalPaymentAmount);
            return loan;
        }
    }
}
