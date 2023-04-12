using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class JourneyFlightsCreate
    {
        const string query = @"INSERT INTO JOURNEY_FLIGHT (JOURNEY_ID, FLIGHT_ID) 
                                OUTPUT INSERTED.ID
                                VALUES (@JOURNEY_ID, @FLIGHT_ID);";
        private readonly int journeyId;
        private readonly int flightId;

        public JourneyFlightsCreate(int journeyId, int flightId)
        {
            this.journeyId = journeyId;
            this.flightId = flightId;
        }

        public int Process()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@JOURNEY_ID", journeyId);
            sqlCommand.Parameters.AddWithValue("@FLIGHT_ID", flightId);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                int idResult = Convert.ToInt32(sqlDataReader["ID"]);
                sqlConnection.Close();
                return idResult;
            }
            else
            {
                sqlConnection.Close();
                return 0;
            }
        }
    }
}

