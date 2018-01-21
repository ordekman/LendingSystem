using System;
using LendingSystem.Extensions;

namespace LendingSystem.Models
{
    class Loan: ILoan
    {
        private readonly uint _periodsNumber;

        private decimal _paymentAmountPerPeriod;
        private decimal _totalPaymentAmount;
        private readonly decimal _effectiveInterestRatePerPeriod;

        public Loan(decimal amount, decimal annualInterestRate, uint periodsNumber, uint periodsPerYear)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));

            Amount = amount;
            AnnualInterestRate = annualInterestRate;
            _periodsNumber = periodsNumber;

            _effectiveInterestRatePerPeriod = (decimal)Math.Pow(1 + (double)annualInterestRate, 1 / (double)periodsPerYear) - 1;
            ComputeTotalPaymentAmount();
        }

        public decimal PaymentAmountPerPeriod => _paymentAmountPerPeriod;

        public decimal TotalPaymentAmount => _totalPaymentAmount;

        public decimal AnnualInterestRate { get; }

        public decimal Amount { get; }

        private void ComputeTotalPaymentAmount()
        {
            if (AnnualInterestRate == 0)
            {
                _paymentAmountPerPeriod = Amount / _periodsNumber;
                _totalPaymentAmount = Amount;
                return;
            }

            _paymentAmountPerPeriod = _effectiveInterestRatePerPeriod * Amount / (1 - 1/(1 + _effectiveInterestRatePerPeriod).Pow(_periodsNumber));
            _totalPaymentAmount = _periodsNumber * _paymentAmountPerPeriod;
        }
    }
}
