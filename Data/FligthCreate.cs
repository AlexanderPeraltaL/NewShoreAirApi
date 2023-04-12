using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FlightCreate
    {
        const string query = @"INSERT INTO FLIGHT (TRANSPORT_ID, DEPARTURE_STATION, ARRIVAL_STATION, PRICE)
                            OUTPUT INSERTED.ID, INSERTED.TRANSPORT_ID, INSERTED.DEPARTURE_STATION, INSERTED.PRICE
                            VALUES (@TRANSPORT_ID, @DEPARTURE_STATION, @ARRIVAL_STATION, @PRICE);";
        private readonly Flight flight;

        public FlightCreate(Flight flight)
        {
            this.flight = flight;
        }

        public int Process()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@TRANSPORT_ID", flight.Transport.Id);
            sqlCommand.Parameters.AddWithValue("@DEPARTURE_STATION", flight.DepartureStation);
            sqlCommand.Parameters.AddWithValue("@ARRIVAL_STATION", flight.ArrivalStation);
            sqlCommand.Parameters.AddWithValue("@PRICE", flight.Price);

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

