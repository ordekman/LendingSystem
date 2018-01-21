using System;
using System.Collections.Generic;
using System.Linq;
using LendingSystem.Data;
using LendingSystem.Exceptions;
using LendingSystem.Models;

namespace LendingSystem
{
    /// <inheritdoc />
    public class LendingSystem: ILendingSystem
    {
        private readonly ILendersDal _lendersDal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lendersDal">Data access layer to access lenders</param>
        public LendingSystem(ILendersDal lendersDal)
        {
            _lendersDal = lendersDal ?? throw new System.ArgumentNullException(nameof(lendersDal));
        }

        /// <inheritdoc />
        public bool HasEnoughResources(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));

            IEnumerable<Lender> allLenders = _lendersDal.GetAllLenders();
            return allLenders.Sum(l => l.AvailableAmount) >= amount;
        }

        /// <inheritdoc />
        public ILoan Lend(decimal amount, uint periodsNumber = 36, uint periodsPerYear = 12)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));

            if (!HasEnoughResources(amount))
            {
                throw new LendingSystemException("Not enough resources to lend!");
            }

            List<ILoan> loans = new List<ILoan>();

            IEnumerable<Lender> allLenders = _lendersDal.GetAllLenders();

            decimal requestedAmount = amount;

            foreach (Lender lender in allLenders.OrderBy(l => l.AnnualInterestRate))
            {
                if (lender.AvailableAmount >= requestedAmount)
                {
                    loans.Add(new Loan(requestedAmount, lender.AnnualInterestRate, periodsNumber, periodsPerYear));
                    lender.AvailableAmount = lender.AvailableAmount - requestedAmount;
                    break;
                }

                requestedAmount -= lender.AvailableAmount;
                loans.Add(new Loan(lender.AvailableAmount, lender.AnnualInterestRate, periodsNumber, periodsPerYear));
                lender.AvailableAmount = 0;
            }

            if (loans.Count == 1)
            {
                return loans[0];
            }
            return new CumulativeLoan(loans);
        }
    }
}
