using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingRestSharp
{
    class Database
    {
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

        public static void InsertStockHistory(dynamic stock)
        {
            // SelectTop5Stock(_connectionString);
            InsertIntoScrapeHistory(stock);
        }

        public static void InsertCurrentStock(dynamic stock)
        {
            InsertIntoLatestScrape(stock);
        }
        public static void Clear_Reset()
        {
            DeleteTableData();
            ResetAutoIncrementer();
        }

        private static void InsertIntoLatestScrape(dynamic stock)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM WorldTradeStockCurrent WHERE Symbol = @Symbol)
                                        UPDATE WorldTradeStockCurrent
                                        SET Name = @Name, Price = @Price, Change = @Change, ChangePercent = @ChangePercent
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO WorldTradeStockCurrent VALUES(@Name, @Symbol, @Price, @Change, @ChangePercent);";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(latestScrape, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                        command.Parameters.Add(new SqlParameter("@Price", stock.Price));
                        command.Parameters.Add(new SqlParameter("@Change", stock.Change));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", stock.ChangePercent));
                       
                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to WorldTradeStockCurrent table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
            }
        }

        private static void InsertIntoScrapeHistory(dynamic stock)
        {
            string scrapeHistory = "INSERT INTO WorldTradeStockHistory VALUES (@Name, @Symbol, @Price, @Change, @ChangePercent);";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(scrapeHistory, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@Price", stock.Price));
                        command.Parameters.Add(new SqlParameter("@Change", stock.Change));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", stock.ChangePercent));
                       
                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to WorldTradeStockHistory table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
            }
        }

        public static void SelectTop5Stock(string connection)
        {
            //Symbol, Name, Price, Change, ChangePercent
            string databaseQuery = "SELECT * FROM WorldTradeStockHistory" +
                                   "WHERE MAX(Id)" +
                                   "LIMIT 3";
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(databaseQuery, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4}", (string)reader["Symbol"],
                                (string)reader["Name"], (string)reader["Price"], (string)reader["Change"],
                                (string)reader["ChangePercent"]);
                        };
                        reader.NextResult();
                    }
                }
                con.Close();
            }
        }

        public static void DeleteTableData()
        {
            string deleteTableData = "DELETE FROM WorldTradeStockHistory;";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(deleteTableData, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }

        public static void ResetAutoIncrementer()
        {
            string reseed = "DBCC CHECKIDENT ('WorldTradeStockHistory', RESEED, 0);";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(reseed, con))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Auto incrementer reset...");
                    }
                }
                con.Close();
            }
        }
    }
}
