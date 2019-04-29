using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.HAPScraperService
{
    public class Database
    {
        public static void InsertStockDataIntoDatabase(Stock stock)
        {
            string connectionString = null;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

            // DeleteTableData(connectionString);
            //    ResetAutoIncrementer(connectionString);        

            InsertIntoLatestScrape(stock, connectionString);
            InsertIntoSrapeHistory(stock, connectionString);
        }

        private static void InsertIntoLatestScrape(Stock stock, string connectionString)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM NasdaqStockCurrent WHERE Symbol = @Symbol)
                                        UPDATE NasdaqStockCurrent
                                        SET Name = @Name, Price = @Price, Change = @Change
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO NasdaqStockCurrent VALUES(@Name, @Symbol, @Price, @Change);";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(latestScrape, con))
                        {
                            command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                            command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                            command.Parameters.Add(new SqlParameter("@Price", stock.LastPrice));
                            command.Parameters.Add(new SqlParameter("@Change", stock.Change));

                            command.ExecuteNonQuery();
                            Console.WriteLine("{0} added to NasdaqStockCurrent table...", stock.Name);
                        }
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine("Error inserting stock into database...");
                        throw error;
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
                if (con.State == System.Data.ConnectionState.Closed)
                    Console.WriteLine("Connection sucessfully closed...");
            }
        }

        private static void InsertIntoSrapeHistory(Stock stock, string connectionString)
        {
            string scrapeHistory = "INSERT INTO NasdaqStockHistory VALUES(@Name, @Symbol, @Price, @Change);";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(scrapeHistory, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@Price", stock.LastPrice));
                        command.Parameters.Add(new SqlParameter("@Change", stock.Change));

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to NasdaqStockHistory table...", stock.Name);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                con.Close();
                if (con.State == System.Data.ConnectionState.Closed)
                    Console.WriteLine("Connection sucessfully closed...");
            }
        }
    }
}