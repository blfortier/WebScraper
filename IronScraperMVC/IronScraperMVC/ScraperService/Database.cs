using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IronScraperMVC.ScraperService
{
    public class Database
    {
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

        public static void InsertStockDataIntoDB(Stock stock)
        {
            InsertIntoLatestScrape(stock, _connectionString);
            InsertIntoScrapeHistory(stock, _connectionString);
        }

        public static void Clear_Reset()
        {
            DeleteTableData(_connectionString);
            ResetAutoIncrementer(_connectionString);
        }

        private static void InsertIntoLatestScrape(Stock stock, string connectionString)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM FinancialContentCurrent WHERE Symbol = @Symbol)
                                        UPDATE FinancialContentCurrent
                                        SET Name = @Name, Price = @Price, PriceChange = @PriceChange, ChangePercent = @ChangePercent, Volume = @Volume
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO FinancialContentCurrent VALUES(@Name, @Symbol, @Price, @PriceChange, @ChangePercent, @Volume);";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(latestScrape, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@Price", stock.Price));
                        command.Parameters.Add(new SqlParameter("@PriceChange", stock.PriceChange));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", stock.ChangePercent));
                        command.Parameters.Add(new SqlParameter("@Volume", stock.Volume));

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to FinancialContentCurrent table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
            }
        }

        private static void InsertIntoScrapeHistory(Stock stock, string connectionString)
        {
            string scrapeHistory = "INSERT INTO FinancialContentHistory VALUES (@Name, @Symbol, @Price, @PriceChange, @ChangePercent, @Volume);";

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
                        command.Parameters.Add(new SqlParameter("@PriceChange", stock.PriceChange));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", stock.ChangePercent));
                        command.Parameters.Add(new SqlParameter("@Volume", stock.Volume));

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to FinancialContentHistory table...", stock.Symbol);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
            }
        }

        public static void DeleteTableData(string connection)
        {
            string deleteTableData = "DELETE FROM FinancialContentHistory;";
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
            string reseed = "DBCC CHECKIDENT ('FinancialContentHistory', RESEED, 0);";

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