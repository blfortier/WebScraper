using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ScraperUsingSelenium
{
    class Database
    {
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

        public static void InsertStockHistory(Stock stock)
        {
            InsertIntoScrapeHistory(stock);
        }

        public static void InsertCurrentStock(Stock stock)
        {
            InsertIntoLatestScrape(stock);
        }

        public static void Clear_Reset()
        {
            DeleteTableData();
            ResetAutoIncrementer();
        }

        private static void InsertIntoLatestScrape(Stock stock)
        { 

            string latestScrape = @"IF EXISTS(SELECT* FROM Stocks WHERE Symbol = @Symbol)
                                        UPDATE Stocks
                                        SET LastPrice = @LastPrice, Change = @Change, ChangePercent = @ChangePercent,
                                            Volume = @Volume, AvgVol = @AvgVol, MarketCap = @MarketCap 
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO Stocks VALUES(@Symbol, @LastPrice, @Change, @ChangePercent, @Volume, @AvgVol, @MarketCap);";


            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(latestScrape, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@LastPrice", stock.LastPrice));
                        command.Parameters.Add(new SqlParameter("@Change", stock.Change));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", stock.ChangePercent));
                        command.Parameters.Add(new SqlParameter("@Volume", stock.Volume));
                        command.Parameters.Add(new SqlParameter("@AvgVol", stock.AvgVol));
                        command.Parameters.Add(new SqlParameter("@MarketCap", stock.MarketCap));

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to Stocks table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
            }
        }

        private static void InsertIntoScrapeHistory(Stock stock)
        {
            string scrapeHistory = "INSERT INTO StockHistory VALUES (@Symbol, @LastPrice, @Change, @ChangePercent, @Volume, @AvgVol, @MarketCap);";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(scrapeHistory, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@LastPrice", stock.LastPrice));
                        command.Parameters.Add(new SqlParameter("@Change", stock.Change));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", stock.Change));
                        command.Parameters.Add(new SqlParameter("@Volume", stock.Volume));
                        command.Parameters.Add(new SqlParameter("@AvgVol", stock.AvgVol));
                        command.Parameters.Add(new SqlParameter("@MarketCap", stock.MarketCap));

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to StockHistory table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
            }
        }

        public static void DeleteTableData()
        {
            string deleteTableData = "DELETE FROM StockHistory;";

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
            string reseed = "DBCC CHECKIDENT ('StockHistory', RESEED, 0);";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(reseed, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }
    }
}
