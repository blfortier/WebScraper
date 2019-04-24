﻿using System;
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
        public static void InsertStockDataIntoDatabase(Stock stock)
        {
            string connectionString = null;
            //connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";

            // DeleteTableData(connectionString);
            //    ResetAutoIncrementer(connectionString);        

            InsertIntoLatestScrape(stock, connectionString);
            InsertIntoSrapeHistory(stock, connectionString);
        }

        private static void InsertIntoLatestScrape(Stock stock, string connectionString)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM NasdaqStockCurrent WHERE Symbol = @Symbol)
                                        UPDATE Stocks
                                        SET Name = @Name, LastPrice = @LastPrice, Change = @Change
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO NasdaqStockCurrent VALUES(@Name, @Symbol, @LastPrice, @Change);";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    // Console.WriteLine("Connection open...");

                    try
                    {
                        using (SqlCommand command = new SqlCommand(latestScrape, con))
                        {
                            command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                            command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                            command.Parameters.Add(new SqlParameter("@LastPrice", stock.LastPrice));
                            command.Parameters.Add(new SqlParameter("@Change", stock.Change));

                            command.ExecuteNonQuery();
                            Console.WriteLine("{0} added to NasdaqStockCurrent table...", stock.Name);
                            // DeleteTableData(con);
                            //DeleteTableData(con);
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
                //con.Close();
                //if (con.State == System.Data.ConnectionState.Closed)
                //    Console.WriteLine("Connection sucessfully closed...");
            }
        }

        private static void InsertIntoSrapeHistory(Stock stock, string connectionString)
        {
            string scrapeHistory = "INSERT INTO NasdaqStockHistory VALUES(@Name, @Symbol, @LastPrice, @Change);";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(scrapeHistory, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", stock.Name));
                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        command.Parameters.Add(new SqlParameter("@LastPrice", stock.LastPrice));
                        command.Parameters.Add(new SqlParameter("@Change", stock.Change));

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to NasdaqStockHistory table...", stock.Name);
                    }
                }
                else
                {
                    Console.WriteLine("No connection...");
                }
                //con.Close();
                //if (con.State == System.Data.ConnectionState.Closed)
                //    Console.WriteLine("Connection sucessfully closed...");
            }
        }
    }
}
