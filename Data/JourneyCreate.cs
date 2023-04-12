using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class JourneyCreate
    {
        const string query = @"INSERT INTO JOURNEY (ORIGIN, DESTINATION, PRICE)
                            OUTPUT INSERTED.ID, INSERTED.ORIGIN, INSERTED.DESTINATION, INSERTED.PRICE
                            VALUES (@ORIGIN, @DESTINATION, @PRICE);";
        private readonly Journey journey;

        public JourneyCreate(Journey journey)
        {
            this.journey = journey;
        }

        public int Process()
        {
            SqlConnection sqlConnection = new SqlConnection(Connection.SqlString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ORIGIN", journey.Origin);
            sqlCommand.Parameters.AddWithValue("@DESTINATION", journey.Destination);
            sqlCommand.Parameters.AddWithValue("@PRICE", journey.Price);

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

