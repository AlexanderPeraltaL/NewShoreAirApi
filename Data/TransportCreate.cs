using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    public class TransportCreate
    {
        const string query = @"INSERT INTO TRANSPORT (FLIGHT_CARRIER, FLIGHT_NUMBER) 
                                OUTPUT INSERTED.ID, INSERTED.FLIGHT_CARRIER, INSERTED.FLIGHT_NUMBER
                                VALUES (@FLIGHT_CARRIER, @FLIGHT_NUMBER);";
        private readonly Transport transport;

        public TransportCreate(Transport transport)
        {
            this.transport = transport;
        }
        public int Process()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FLIGHT_CARRIER", transport.FlightCarrier);
            sqlCommand.Parameters.AddWithValue("@FLIGHT_NUMBER", transport.FlightNumber);
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
