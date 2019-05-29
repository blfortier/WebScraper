using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowPages
{
    public class Database
    {
        private const string  _connectionString= @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestaurantData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void InsertRestaurantDataIntoTable()
        {
            string sqlStatement = @"IF EXISTS(SELECT * FROM RestaurantData WHERE RestName = @RestName)
                                      UPDATE RestaurantData
                                      SET RestName = @RestName, Address = @Address,
                                          CityState = @CityState, PhoneNumber = @PhoneNumber
                                      WHERE RestName = @RestName
                                  ELSE
                                    INSERT INTO RestaurantData VALUES(@RestName, @Address,
                                                @CityState, @PhoneNumber);";


            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand command = new SqlCommand(sqlStatement))
                        {
                            command.Parameters.Add(new SqlParameter("@RestName", ))
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There's an error connecting with the database...");
                throw e;
            }

        }

    }
}
