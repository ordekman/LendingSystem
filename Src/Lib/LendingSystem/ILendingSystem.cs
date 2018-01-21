using LendingSystem.Models;

namespace LendingSystem
{
    /// <summary>
    /// Interface representanting lending system
    /// </summary>
    public interface ILendingSystem
    {
        /// <summary>
        /// Determines whether system has enough resources to lend requested amount
        /// </summary>
        /// <param name="amount">requested amount</param>
        /// <returns>true if has enough resources, otherwise false</returns>
        bool HasEnoughResources(decimal amount);

        /// <summary>
        /// Lends requested amount
        /// </summary>
        /// <param name="amount">requested amount</param>
        /// <param name="periodsNumber">how many payments will be executed in order to repay a loan</param>
        /// <param name="periodsPerYear">how many periods are within 1 year</param>
        /// <returns>Loan containing all important information about it</returns>
        ILoan Lend(decimal amount, uint periodsNumber = 36, uint periodsPerYear = 12);
    }
}
