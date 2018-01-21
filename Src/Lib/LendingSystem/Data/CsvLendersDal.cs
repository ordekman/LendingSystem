using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using LendingSystem.Models;

namespace LendingSystem.Data
{
    /// <summary>
    /// Class responsible for accessing Lenders from specific CSV file
    /// </summary>
    public class CsvLendersDal: ILendersDal
    {
        private readonly string _csvFilePath;
        private readonly Lazy<IEnumerable<Lender>> _lenders;

        private IEnumerable<Lender> LoadLendersFromCsvFile()
        {
            using (TextReader fileReader = File.OpenText(_csvFilePath))
            {
                var csv = new CsvReader(fileReader);
                csv.Configuration.RegisterClassMap<CsvMap>();
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;

                return csv.GetRecords<Lender>().ToList();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="csvFilePath">Relative path to csv file</param>
        public CsvLendersDal(string csvFilePath)
        {
            _csvFilePath = csvFilePath ?? throw new ArgumentNullException(nameof(csvFilePath));
            if (!File.Exists(csvFilePath))
                throw new ArgumentException($"File {csvFilePath} does not exist!");

            _lenders = new Lazy<IEnumerable<Lender>>(LoadLendersFromCsvFile);
        }

        /// <summary>
        /// Loads Lenders from csv file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Lender> GetAllLenders()
        {
            return _lenders.Value;
        }
    }
}
