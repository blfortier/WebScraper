using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperUsingHAP
{
    class Database
    {
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

        public static void InsertStocksIntoDB(Stock stock)
        {
            InsertIntoScrapeHistory(stock);
            InsertIntoLatestScrape(stock);
        }

        public static void Clear_Reset()
        {
            DeleteTableData();
            ResetAutoIncrementer();
        }

        private static void InsertIntoLatestScrape(Stock stock)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM NasdaqStockCurrent WHERE Symbol = @Symbol)
                                        UPDATE NasdaqStockCurrent
                                        SET Name = @Name, Price = @Price, Change = @Change
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO NasdaqStockCurrent VALUES(@Name, @Symbol, @Price, @Change);";


            using (SqlConnection con = new SqlConnection(_connectionString))
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
            }
        }

        private static void InsertIntoScrapeHistory(Stock stock)
        {
            string scrapeHistory = "INSERT INTO NasdaqStockHistory VALUES(@Name, @Symbol, @Price, @Change);";

            using (SqlConnection con = new SqlConnection(_connectionString))
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
            string deleteTableData = "DELETE FROM NasdaqStockHistory;";
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
            string reseed = "DBCC CHECKIDENT ('NasdaqStockHistory', RESEED, 0);";

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
