using System;
using System.Collections.Generic;
using System.Linq;

namespace LendingSystem.Models
{
    class CumulativeLoan: ILoan
    {
        private readonly IEnumerable<ILoan> _loans;

        public CumulativeLoan(IEnumerable<ILoan> loans)
        {
            _loans = loans ?? throw new ArgumentNullException(nameof(loans));
        }

        public decimal PaymentAmountPerPeriod
        {
            get { return _loans.Sum(l => l.PaymentAmountPerPeriod); }
        }

        public decimal TotalPaymentAmount
        {
            get { return _loans.Sum(l => l.TotalPaymentAmount); }
        }

        public decimal AnnualInterestRate => ComputeAnnualInterestRate();

        public decimal Amount
        {
            get { return _loans.Sum(l => l.Amount); }
        }

        private decimal ComputeAnnualInterestRate()
        {
            decimal interestRate = 0;
            var totalAmount = Amount;
            foreach (ILoan loan in _loans)
            {
                interestRate += loan.Amount / totalAmount * loan.AnnualInterestRate;
            }
            return interestRate;
        }
    }
}
