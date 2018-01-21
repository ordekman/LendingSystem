using System;
using System.Collections.Generic;
using LendingSystem.Data;
using LendingSystem.Exceptions;
using LendingSystem.Models;
using Moq;
using NUnit.Framework;

namespace LendingSystem.Tests
{
    [TestFixture]
    class LendingSystemTest
    {
        private ILendingSystem _lendingSystem;
        private Mock<ILendersDal> _lendersDalMock;
        private Lender _lender1;
        private Lender _lender2;
        private Lender _lender3;

        [SetUp]
        public void SetUp()
        {
            _lendersDalMock = new Mock<ILendersDal>();
            _lender1 = new Lender {AnnualInterestRate = 0.01M, AvailableAmount = 100, Name = "TestLender1"};
            _lender2 = new Lender { AnnualInterestRate = 0.02M, AvailableAmount = 200, Name = "TestLender2" };
            _lender3 = new Lender { AnnualInterestRate = 0.03M, AvailableAmount = 300, Name = "TestLender3" };
            _lendersDalMock.Setup(m => m.GetAllLenders()).Returns(new List<Lender> {_lender3, _lender2, _lender1});

            _lendingSystem = new LendingSystem(_lendersDalMock.Object);
        }

        [Test]
        public void Lend_FirstLenderHasEnoughResources_SingleLoanIsLent()
        {
            ILoan loan = _lendingSystem.Lend(100);

            _lendersDalMock.Verify(m => m.GetAllLenders(), Times.Exactly(2));
            Assert.IsNotNull(loan);
            Assert.AreEqual(typeof(Loan), loan.GetType());
            Assert.AreEqual(100, loan.Amount);
            Assert.AreEqual(0.01, loan.AnnualInterestRate);
            Assert.AreEqual(0, _lender1.AvailableAmount);
        }

        [Test]
        public void Lend_SecondLenderHasEnoughResources_CumulativeLoanIsLent()
        {
            ILoan loan = _lendingSystem.Lend(150);

            _lendersDalMock.Verify(m => m.GetAllLenders(), Times.Exactly(2));
            Assert.IsNotNull(loan);
            Assert.AreEqual(typeof(CumulativeLoan), loan.GetType());
            Assert.AreEqual(150, loan.Amount);
            Assert.AreEqual(0, _lender1.AvailableAmount);
            Assert.AreEqual(150, _lender2.AvailableAmount);
        }

        [Test]
        public void Lend_ThirdLenderHasEnoughResources_CumulativeLoanIsLent()
        {
            ILoan loan = _lendingSystem.Lend(450);

            _lendersDalMock.Verify(m => m.GetAllLenders(), Times.Exactly(2));
            Assert.IsNotNull(loan);
            Assert.AreEqual(typeof(CumulativeLoan), loan.GetType());
            Assert.AreEqual(450, loan.Amount);
            Assert.AreEqual(0, _lender1.AvailableAmount);
            Assert.AreEqual(0, _lender2.AvailableAmount);
            Assert.AreEqual(150, _lender3.AvailableAmount);
        }

        [Test]
        public void Lend_NotEnoughResources_ExceptionIsThrown()
        {
            Assert.Throws<LendingSystemException>(() => _lendingSystem.Lend(700)); 
        }

        [Test]
        public void Lend_AmountIsZero_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _lendingSystem.Lend(0));
        }

        [Test]
        public void HasEnoughResources_HasEnoughResources_TrueIsReturned()
        {
            bool hasEnoughResources = _lendingSystem.HasEnoughResources(600);
            Assert.IsTrue(hasEnoughResources);
        }

        [Test]
        public void HasEnoughResources_DoesNotHaveEnoughResources_FalseIsReturned()
        {
            bool hasEnoughResources = _lendingSystem.HasEnoughResources(601);
            Assert.IsFalse(hasEnoughResources);
        }

        [Test]
        public void HasEnoughResources_AmountIsZero_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _lendingSystem.HasEnoughResources(0));
        }

        [Test]
        public void Constructor_DalIsNull_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new LendingSystem(null));
        }
    }
}
