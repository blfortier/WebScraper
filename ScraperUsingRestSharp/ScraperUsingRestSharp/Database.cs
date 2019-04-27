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
        public static void InsertStockDataIntoDatabase(dynamic stock)
        {
            string connectionString = null;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";


            //    DeleteTableData(connectionString);
            //    ResetAutoIncrementer(connectionString);        

            InsertIntoLatestSrape(stock, connectionString);
            InsertIntoSrapeHistory(stock, connectionString);
        }

        private static void InsertIntoLatestSrape(dynamic stock, string connectionString)
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
                    // Console.WriteLine("Connection open...");

                    using (SqlCommand command = new SqlCommand(latestScrape, con))
                    {
                        if (stock.Symbol == null)
                            Console.WriteLine("wth");
                        else
                            Console.WriteLine("idk");

                        command.Parameters.Add(new SqlParameter("@Symbol", stock.Symbol));
                        Console.WriteLine(stock.Name.Length);
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

                if (con.State == System.Data.ConnectionState.Closed)
                    Console.WriteLine("Connection sucessfully closed...");
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
                if (con.State == System.Data.ConnectionState.Closed)
                    Console.WriteLine("Connection sucessfully closed...");
            }
        }

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
            }
        }
    }
}
