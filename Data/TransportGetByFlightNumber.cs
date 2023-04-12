using Model;
using System.Data.SqlClient;

namespace Data
{
    public class TransportGetByFlightNumber
    {
        const string query = @"SELECT ID, FLIGHT_CARRIER, FLIGHT_NUMBER
                               FROM TRANSPORT WHERE FLIGHT_NUMBER = @FLIGHT_NUMBER";

        private readonly Transport transport;
        private Transport transportResult;

        public TransportGetByFlightNumber(Transport transport)
        {
            this.transport = transport;
            transportResult = new Transport();
        }
        public Transport Process()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FLIGHT_NUMBER", transport.FlightNumber);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                transportResult.Id = Convert.ToInt32(sqlDataReader["ID"]);
                transportResult.FlightCarrier = Convert.ToString(sqlDataReader["FLIGHT_CARRIER"]);
                transportResult.FlightNumber = Convert.ToString(sqlDataReader["FLIGHT_NUMBER"]);
            }
            sqlConnection.Close();

            return transportResult;
        }
    }
}
