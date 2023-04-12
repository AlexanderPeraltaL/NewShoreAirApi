using Model;
using System.Data.SqlClient;

namespace Data
{
    public class JourneyGetByOriginAndDestination
    {
        const string query = @"
                            SELECT F.DEPARTURE_STATION, F.ARRIVAL_STATION, F.PRICE, T.FLIGHT_CARRIER, 
                            T.FLIGHT_NUMBER, J.ORIGIN, J.DESTINATION, J.PRICE AS TOTAL_PRICE
                            FROM JOURNEY_FLIGHT AS JF
                            INNER JOIN JOURNEY AS J ON JF.JOURNEY_ID = J.ID
                            INNER JOIN FLIGHT AS F ON JF.FLIGHT_ID = F.ID
                            INNER JOIN TRANSPORT AS T ON F.TRANSPORT_ID = T.ID
                            WHERE J.ORIGIN = @ORIGIN AND J.DESTINATION =  @DESTINATION";


        private readonly Journey journey;
        private Journey journeyResponse;

        public JourneyGetByOriginAndDestination(Journey journey)
        {
            this.journey = journey;
            journeyResponse = new Journey();
        }
        public Journey Process()
        {          

            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ORIGIN", journey.Origin);
            sqlCommand.Parameters.AddWithValue("@DESTINATION", journey.Destination);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                journeyResponse.Flights.Add(new Flight
                {
                    DepartureStation = Convert.ToString(sqlDataReader["DEPARTURE_STATION"]),
                    ArrivalStation = Convert.ToString(sqlDataReader["ARRIVAL_STATION"]),
                    Price = Convert.ToDouble(sqlDataReader["PRICE"]),
                    Transport = new Transport
                    {
                        FlightCarrier = Convert.ToString(sqlDataReader["FLIGHT_CARRIER"]),
                        FlightNumber = Convert.ToString(sqlDataReader["FLIGHT_NUMBER"])
                    }
                });
                journeyResponse.Origin = Convert.ToString(sqlDataReader["ORIGIN"]);
                journeyResponse.Destination = Convert.ToString(sqlDataReader["DESTINATION"]);
                journeyResponse.Price = Convert.ToDouble(sqlDataReader["TOTAL_PRICE"]);
            }
            sqlConnection.Close();

            return journeyResponse;
        }
    }
}
