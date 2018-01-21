namespace LendingSystem.Models
{
    /// <summary>
    /// Represents exactly one loan
    /// </summary>
    public interface ILoan
    {
        /// <summary>
        /// How much is necessary to pay in one period
        /// </summary>
        decimal PaymentAmountPerPeriod { get; }

        /// <summary>
        /// How much will be paid in total
        /// </summary>
        decimal TotalPaymentAmount { get; }

        /// <summary>
        /// Annual interest rate (E.g. 0.07 = 7%)
        /// </summary>
        decimal AnnualInterestRate { get; }

        /// <summary>
        /// How much was lent
        /// </summary>
        decimal Amount { get; }
    }
}
