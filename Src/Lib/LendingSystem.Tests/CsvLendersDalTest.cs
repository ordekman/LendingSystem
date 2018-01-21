using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LendingSystem.Data;
using LendingSystem.Models;
using NUnit.Framework;

namespace LendingSystem.Tests
{
    [TestFixture]
    class CsvLendersDalTest
    {
        [Test]
        public void GetAllLenders_FileExists_LendersAreLoaded()
        {
            string fileName = "Market Data for Exercise - csv.csv";

            CsvLendersDal dal = new CsvLendersDal(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
            List<Lender> allLenders = dal.GetAllLenders().ToList();

            Assert.AreEqual(7, allLenders.Count);
            Assert.AreEqual("Bob", allLenders[0].Name);
            Assert.AreEqual(0.075, allLenders[0].AnnualInterestRate);
            Assert.AreEqual(640, allLenders[0].AvailableAmount);
        }

        [Test]
        public void Constructor_PathIsNull_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvLendersDal(null));
        }

        [Test]
        public void Constructor_PathDoesNotExist_ExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new CsvLendersDal("doesNotExist"));
        }
    }
}
