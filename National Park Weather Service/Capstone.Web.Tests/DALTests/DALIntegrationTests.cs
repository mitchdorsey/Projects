using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Web.Models;
using System.Collections.Generic;

namespace Capstone.Web.Tests
{
    /// <summary>
    /// Testing our DAL Methods
    /// Getting data from database, "NPGeek"
    /// </summary>
    [TestClass]
    public class DALIntegrationTests
    {
        private TransactionScope _tran;
        private string _connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = NPGeek; Integrated Security = True";

        /*
        * SETUP.
        */
        [TestInitialize]
        public void Initialize()
        {
            _tran = new TransactionScope();

            //using (SqlConnection conn = new SqlConnection(_connectionString))
            //{
            //    //SqlCommand cmd;
            //    conn.Open();
            //}
        }

        /*
        * CLEANUP.
        * Rollback the Transaction.  
        */
        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        /*
        * TEST.
        */
        [TestMethod]
        public void GetParksForHomePage()
        {
            //Arrange
            NPGeekDAL _dal = new NPGeekDAL(_connectionString);

            //Act
            IList<IndexViewModel> listForHomePage = _dal.GetParksForHomePage();

            //Assert
            Assert.IsNotNull(listForHomePage);
            Assert.AreEqual(10, listForHomePage.Count);
        }

        [TestMethod]
        public void GetAllDetailsByParkCode()
        {
            //Arrange
            NPGeekDAL _dal = new NPGeekDAL(_connectionString);

            //Act
            ParkDetails parkDetailsItem = _dal.GetAllDetailsByParkCode("CVNP");

            //Assert
            Assert.AreEqual("Cuyahoga Valley National Park", parkDetailsItem.ParkName);
        }

        [TestMethod]
        public void GetFiveDayForecast()
        {
            //Arrange
            NPGeekDAL _dal = new NPGeekDAL(_connectionString);

            //Act
            IList<ForecastDay> fiveDayForecast = _dal.GetFiveDayForecast("GSMNP");

            //Assert
            Assert.IsNotNull(fiveDayForecast);
            Assert.AreEqual("partly cloudy", fiveDayForecast[0].Forecast);
        }

        [TestMethod]
        public void AddSurveyToDatabase()
        {
            //Arrange
            NPGeekDAL _dal = new NPGeekDAL(_connectionString);
            SurveyViewModel model = new SurveyViewModel
            {
                ParkCode = "GTNP",
                Email = "smallbutt@bigbutts.net",
                State = "Ohio",
                ActivityLevel = "Inactive"
            };
            
            //Act
            int numRowsAffected = _dal.AddSurveyToDatabase(model);
            

            //Assert
            Assert.AreEqual(1, numRowsAffected);
        }

        [TestMethod]
        public void GetSurveyResults()
        {
            //Arrange
            NPGeekDAL _dal = new NPGeekDAL(_connectionString);

            //Act
            IList<SurveyResultsViewModel> surveyResults = _dal.GetSurveyResults();

            //Assert
            Assert.IsNotNull(surveyResults);
            Assert.AreEqual("Cuyahoga Valley National Park", surveyResults[0].ParkName);
        }
    }
}
