using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcSeleniumScraper.ScraperService
{
    public class Database
    {
        public static void InsertStockDataIntoDatabase(Stocks stock)
        {
            string connectionString = null;
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockData;Integrated Security=True";


            // DeleteTableData(connectionString);
            //    ResetAutoIncrementer(connectionString);        

            InsertIntoLatestSrape(stock, connectionString);
            InsertIntoSrapeHistory(stock, connectionString);
        }

        private static void InsertIntoLatestSrape(Stocks stock, string connectionString)
        {

            string latestScrape = @"IF EXISTS(SELECT* FROM Stocks WHERE Symbol = @Symbol)
                                        UPDATE Stocks
                                        SET LastPrice = @LastPrice, Change = @Change, ChangePercent = @ChangePercent,
                                            MarketTime = @MarketTime, Volume = @Volume,
                                            AvgVol = @AvgVol, Shares = @Shares, MarketCap = @MarketCap 
                                        WHERE Symbol = @Symbol 
                                    ELSE
                                        INSERT INTO Stocks VALUES(@Symbol, @LastPrice, @Change, @ChangePercent, @MarketTime, @Volume, @AvgVol, @Shares, @MarketCap);";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    // Console.WriteLine("Connection open...");

                    using (SqlCommand command = new SqlCommand(latestScrape, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@LastPrice", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@Change", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@MarketTime", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@AvgVol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Shares", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@MarketCap", SqlDbType.VarChar));

                        command.Parameters["@Symbol"].Value = stock.Symbol;
                        command.Parameters["@LastPrice"].Value = stock.LastPrice;
                        command.Parameters["@Change"].Value = stock.Change;
                        command.Parameters["@ChangePercent"].Value = stock.ChangePercent;
                        command.Parameters["@MarketTime"].Value = stock.MarketTime;
                        command.Parameters["@Volume"].Value = stock.Volume;
                        command.Parameters["@AvgVol"].Value = stock.AvgVol;
                        command.Parameters["@Shares"].Value = stock.Shares;
                        command.Parameters["@MarketCap"].Value = stock.MarketCap;

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to Stocks table...", stock.Symbol);
                        // DeleteTableData(con);
                        //DeleteTableData(con);
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

        private static void InsertIntoSrapeHistory(Stocks stock, string connectionString)
        {
            string scrapeHistory = "INSERT INTO StockHistory VALUES (@Symbol, @LastPrice, @Change, @ChangePercent, @MarketTime, @Volume, @AvgVol, @Shares, @MarketCap);";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand command = new SqlCommand(scrapeHistory, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Symbol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@LastPrice", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@Change", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@ChangePercent", SqlDbType.Float));
                        command.Parameters.Add(new SqlParameter("@MarketTime", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Volume", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@AvgVol", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@Shares", SqlDbType.VarChar));
                        command.Parameters.Add(new SqlParameter("@MarketCap", SqlDbType.VarChar));

                        command.Parameters["@Symbol"].Value = stock.Symbol;
                        command.Parameters["@LastPrice"].Value = stock.LastPrice;
                        command.Parameters["@Change"].Value = stock.Change;
                        command.Parameters["@ChangePercent"].Value = stock.ChangePercent;
                        command.Parameters["@MarketTime"].Value = stock.MarketTime;
                        command.Parameters["@Volume"].Value = stock.Volume;
                        command.Parameters["@AvgVol"].Value = stock.AvgVol;
                        command.Parameters["@Shares"].Value = stock.Shares;
                        command.Parameters["@MarketCap"].Value = stock.MarketCap;

                        command.ExecuteNonQuery();
                        Console.WriteLine("{0} added to StockHistory table...", stock.Symbol);
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