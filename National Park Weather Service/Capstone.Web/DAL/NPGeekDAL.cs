using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class NPGeekDAL : INPGeekDAL
    {
        private string connectionString;

        public NPGeekDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<IndexViewModel> GetParksForHomePage()
        {
            List<IndexViewModel> parksForHomePage = new List<IndexViewModel>();

            string query = @"SELECT parkCode, parkName, state, parkDescription FROM park";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    parksForHomePage.Add(MapRowToParkForHomePage(reader));
                }
            }

            return parksForHomePage;
        }

        private IndexViewModel MapRowToParkForHomePage(SqlDataReader reader)
        {
            return new IndexViewModel()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                State = Convert.ToString(reader["state"]),
                ParkDescription = Convert.ToString(reader["parkDescription"])
            };
        }

        public ParkDetails GetAllDetailsByParkCode(string parkCode)
        {
            ParkDetails parkDetails = new ParkDetails();

            string query = @"SELECT * FROM park WHERE parkCode = @parkCode";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    parkDetails = MapRowToParkDetails(reader);
                }
            }

            return parkDetails;
        }

        private ParkDetails MapRowToParkDetails(SqlDataReader reader)
        {
            return new ParkDetails()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                State = Convert.ToString(reader["state"]),
                Acreage = Convert.ToInt32(reader["acreage"]),
                ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]),
                MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]),
                NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]),
                Climate = Convert.ToString(reader["climate"]),
                YearFounded = Convert.ToInt32(reader["YearFounded"]),
                AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]),
                InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]),
                ParkDescription = Convert.ToString(reader["parkDescription"]),
                EntryFee = Convert.ToInt32(reader["entryFee"]),
                NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"])
        };
        }

        public IList<ForecastDay> GetFiveDayForecast(string parkCode)
        {
            List<ForecastDay> fiveDayForecast = new List<ForecastDay>();

            string query = @"SELECT fiveDayForecastValue, low, high, forecast FROM weather WHERE parkCode = @parkCode";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fiveDayForecast.Add(MapRowToForecastDay(reader));
                }
            }

            return fiveDayForecast;
        }

        private ForecastDay MapRowToForecastDay(SqlDataReader reader)
        {
            return new ForecastDay()
            {
                Low = Convert.ToInt32(reader["low"]),
                High = Convert.ToInt32(reader["high"]),
                Forecast = Convert.ToString(reader["forecast"])
            };
        }

        public int AddSurveyToDatabase(SurveyViewModel model)
        {
            int numRowsAffected = 0;

            string query = @"INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@ParkCode, @Email, @State, @ActivityLevel)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ParkCode", model.ParkCode);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@State", model.State);
                cmd.Parameters.AddWithValue("@ActivityLevel", model.ActivityLevel);
                numRowsAffected = (int)cmd.ExecuteNonQuery();
            }

            return numRowsAffected;
        }

        public IList<SurveyResultsViewModel> GetSurveyResults()
        {
            List<SurveyResultsViewModel> surveyResults = new List<SurveyResultsViewModel>();

            string query = @"SELECT park.parkCode, park.parkName, (SELECT COUNT(survey_result.parkCode)) as surveyCount FROM park JOIN survey_result ON park.parkCode = survey_result.parkCode GROUP BY park.parkName, park.parkCode ORDER BY surveyCount DESC, park.parkName ASC;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    surveyResults.Add(MapRowToSurveyResultsViewModel(reader));
                }
            }

            return surveyResults;
        }

        private SurveyResultsViewModel MapRowToSurveyResultsViewModel(SqlDataReader reader)
        {
            return new SurveyResultsViewModel()
            {
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                SurveyCount = Convert.ToInt32(reader["surveyCount"])
            };
        }
    }
}