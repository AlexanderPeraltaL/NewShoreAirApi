using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FlightGetByDepartureAndArrivalStation
    {
        const string query = @"
                            SELECT ID, TRANSPORT_ID, DEPARTURE_STATION, ARRIVAL_STATION, PRICE 
                            FROM FLIGHT WHERE DEPARTURE_STATION = @DEPARTURE_STATION AND 
                            ARRIVAL_STATION = @ARRIVAL_STATION";

        private readonly Flight flight;

        public FlightGetByDepartureAndArrivalStation(Flight flight)
        {
            this.flight = flight;
        }
        public Flight Process()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ARRIVAL_STATION", flight.ArrivalStation);
            sqlCommand.Parameters.AddWithValue("@DEPARTURE_STATION", flight.DepartureStation);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                flight.Id = Convert.ToInt32(sqlDataReader["ID"]);
                flight.DepartureStation = Convert.ToString(sqlDataReader["DEPARTURE_STATION"]);
                flight.ArrivalStation = Convert.ToString(sqlDataReader["ARRIVAL_STATION"]);
                flight.Price = Convert.ToDouble(sqlDataReader["PRICE"]);
                flight.Transport = new Transport { Id = Convert.ToInt32(sqlDataReader["TRANSPORT_ID"]) };                
            }
            sqlConnection.Close();

            return flight;
        }
    }
}
