using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.RestSharpScraperService
{
    public class Database
    {
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

        public static void InsertStockDataIntoDatabase(dynamic stock)
        {
            //  SelectTop5Stock(connectionString);
            InsertIntoLatestScrape(stock, _connectionString);
            InsertIntoSrapeHistory(stock, _connectionString);
        }

        public static void Clear_Reset()
        {
            DeleteTableData(_connectionString);
            ResetAutoIncrementer(_connectionString);
        }

        private static void InsertIntoLatestScrape(dynamic stock, string connectionString)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM WorldTradeStockCurrent WHERE Symbol = @Symbol)
                                        UPDATE WorldTradeStockCurrent
                                        SET Name = @Name, Price = @Price, Change = @Change, ChangePercent = @ChangePercent
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO WorldTradeStockCurrent VALUES(@Name, @Symbol, @Price, @Change, @ChangePercent);";

            using (SqlConnection con = new SqlConnection(connectionString))
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

        private static void InsertIntoSrapeHistory(dynamic stock, string connectionString)
        {
            string scrapeHistory = "INSERT INTO WorldTradeStockHistory VALUES (@Name, @Symbol, @Price, @Change, @ChangePercent);";

            using (SqlConnection con = new SqlConnection(connectionString))
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

        //public static void SelectTop5Stock(string connection)
        //{
        //    //Symbol, Name, Price, Change, ChangePercent
        //    string databaseQuery = "SELECT * FROM WorldTradeStockHistory" +
        //                           "WHERE WorkdTradeStockHistory.row <= 5;";
        //    using (SqlConnection con = new SqlConnection(connection))
        //    {
        //        con.Open();

        //        if (con.State == System.Data.ConnectionState.Open)
        //        {
        //            using (SqlCommand cmd = new SqlCommand(databaseQuery, con))
        //            {
        //                SqlDataReader reader = cmd.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    Console.WriteLine("{0} {1} {2} {3} {4}", (string)reader["Symbol"],
        //                        (string)reader["Name"], (string)reader["Price"], (string)reader["Change"],
        //                        (string)reader["ChangePercent"]);
        //                };
        //                reader.NextResult();                    
        //            }
        //        }
        //        con.Close();
        //    }
        //}

        public static void DeleteTableData(string connection)
        {
            string deleteTableData = "DELETE FROM WorldTradeStockHistory;";
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(deleteTableData, con))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Table cleared...");
                    }
                }
                con.Close();
            }
        }

        public static void ResetAutoIncrementer(string connection)
        {
            string reseed = "DBCC CHECKIDENT ('WorldTradeStockHistory', RESEED, 0);";

            using (SqlConnection con = new SqlConnection(connection))
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