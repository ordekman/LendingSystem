namespace LendingSystem.Models
{
    /// <summary>
    /// Represents exactly one lender
    /// </summary>
    public class Lender
    {
        /// <summary>
        /// Name of lender
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Annual Interest Rate (E.g. 0.07 = 7%)
        /// </summary>
        public decimal AnnualInterestRate { get; set; }

        /// <summary>
        /// How much can lender lend
        /// </summary>
        public decimal AvailableAmount { get; set; }
    }
}
