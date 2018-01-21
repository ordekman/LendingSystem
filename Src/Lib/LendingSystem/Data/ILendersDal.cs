using System.Collections.Generic;
using LendingSystem.Models;

namespace LendingSystem.Data
{
    /// <summary>
    /// Interface for lenders access
    /// </summary>
    public interface ILendersDal
    {
        /// <summary>
        /// Loads all lenders
        /// </summary>
        /// <returns>Loaded lenders</returns>
        IEnumerable<Lender> GetAllLenders();
    }
}
