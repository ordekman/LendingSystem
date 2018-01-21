using System;

namespace LendingSystem.Exceptions
{
    /// <summary>
    /// Landing System Exception
    /// </summary>
    public class LendingSystemException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error message</param>
        public LendingSystemException(string message) : base(message)
        {
        }
    }
}
