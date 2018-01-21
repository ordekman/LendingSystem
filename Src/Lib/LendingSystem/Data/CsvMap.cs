using CsvHelper.Configuration;
using LendingSystem.Models;

namespace LendingSystem.Data
{
    /// <summary>
    /// CSV file header and <see cref="Lender"/> mapping
    /// </summary>
    public sealed class CsvMap: ClassMap<Lender>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CsvMap()
        {
            Map(m => m.AvailableAmount).Name("Available");
            Map(m => m.AnnualInterestRate).Name("Rate");
            Map(m => m.Name).Name("Lender");
        }
    }
}
