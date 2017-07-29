using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Accord.Extensions.Statistics.Filters;
using StockPrediction;

namespace Stocks
{
    public class YahooDataBringer : IDataBringer
    {
  
        public IList BringMeData(string symbol)
        {
            //first get a valid token from Yahoo Finance
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.Refresh();
            }

            List<HistoryPrice> hps = Historical.Get(symbol, DateTime.Now.AddMonths(-1), DateTime.Now);

            return hps.Select(x => new Stock(x.Volume, x.AdjClose, x.Close, x.Low, x.High, x.Open, x.Date)).ToList();
        }
    }

  

    /// <summary>
    /// Class for fetching token (cookie and crumb) from Yahoo Finance
    /// Copyright Dennis Lee
    /// 19 May 2017
    /// 
    /// </summary>
    public class Token
    {
        public static string Cookie { get; set; }
        public static string Crumb { get; set; }

        private static Regex regex_crumb;

        /// <summary>
        /// Refresh cookie and crumb value
        /// </summary>
        /// <param name="symbol">Stock ticker symbol</param>
        /// <returns></returns>
        public static bool Refresh(string symbol = "SPY")
        {

            try
            {
                Token.Cookie = "";
                Token.Crumb = "";

                string url_scrape = "https://finance.yahoo.com/quote/{0}?p={0}";
                //url_scrape = "https://finance.yahoo.com/quote/{0}/history"

                string url = string.Format(url_scrape, symbol);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

                request.CookieContainer = new CookieContainer();
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    string cookie = response.GetResponseHeader("Set-Cookie").Split(';')[0];

                    string html = "";

                    using (Stream stream = response.GetResponseStream())
                    {
                        html = new StreamReader(stream).ReadToEnd();
                    }

                    if (html.Length < 5000)
                        return false;
                    string crumb = getCrumb(html);
                    html = "";

                    if (crumb != null)
                    {
                        Token.Cookie = cookie;
                        Token.Crumb = crumb;
                        Debug.Print("Crumb: '{0}', Cookie: '{1}'", crumb, cookie);
                        return true;
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return false;

        }

        /// <summary>
        /// Get crumb value from HTML
        /// </summary>
        /// <param name="html">HTML code</param>
        /// <returns></returns>
        private static string getCrumb(string html)
        {

            string crumb = null;

            try
            {
                //initialize on first time use
                if (regex_crumb == null)
                    regex_crumb = new Regex("CrumbStore\":{\"crumb\":\"(?<crumb>.+?)\"}",
                        RegexOptions.CultureInvariant | RegexOptions.Compiled);

                MatchCollection matches = regex_crumb.Matches(html);

                if (matches.Count > 0)
                {
                    crumb = matches[0].Groups["crumb"].Value;

                    //fixed unicode character 'SOLIDUS'
                    if (crumb.Length != 11)
                        crumb = crumb.Replace("\\u002F", "/");
                }
                else
                {
                    Debug.Print("Regex no match");
                }

                //prevent regex memory leak
                matches = null;

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            GC.Collect();
            return crumb;

        }


    }

    /// <summary>
    /// Class for fetching stock historical price from Yahoo Finance
    /// Copyright Dennis Lee
    /// 19 May 2017
    /// 
    /// </summary>
    public class Historical
    {

        /// <summary>
        /// Get stock historical price from Yahoo Finance
        /// </summary>
        /// <param name="symbol">Stock ticker symbol</param>
        /// <param name="start">Starting datetime</param>
        /// <param name="end">Ending datetime</param>
        /// <returns>List of history price</returns>
        public static List<HistoryPrice> Get(string symbol, DateTime start, DateTime end)
        {
            List<HistoryPrice> HistoryPrices = new List<HistoryPrice>();

            try
            {
                string csvData = GetRaw(symbol, start, end);
                if (csvData != null)
                    HistoryPrices = Parse(csvData);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return HistoryPrices;

        }

        /// <summary>
        /// Get raw stock historical price from Yahoo Finance
        /// </summary>
        /// <param name="symbol">Stock ticker symbol</param>
        /// <param name="start">Starting datetime</param>
        /// <param name="end">Ending datetime</param>
        /// <returns>Raw history price string</returns>

        public static string GetRaw(string symbol, DateTime start, DateTime end)
        {

            string csvData = null;

            try
            {
                string url = "https://query1.finance.yahoo.com/v7/finance/download/{0}?period1={1}&period2={2}&interval=1d&events=history&crumb={3}";

                //if no token found, refresh it
                if (string.IsNullOrEmpty(Token.Cookie) | string.IsNullOrEmpty(Token.Crumb))
                {
                    if (!Token.Refresh(symbol))
                        return GetRaw(symbol, start, end);
                }

                url = string.Format(url, symbol, Math.Round(DateTimeToUnixTimestamp(start), 0), Math.Round(DateTimeToUnixTimestamp(end), 0), Token.Crumb);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add(HttpRequestHeader.Cookie, Token.Cookie);
                    csvData = wc.DownloadString(url);
                }

            }
            catch (WebException webEx)
            {
                HttpWebResponse response = (HttpWebResponse)webEx.Response;

                //Re-fecthing token
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Debug.Print(webEx.Message);
                    Token.Cookie = "";
                    Token.Crumb = "";
                    Debug.Print("Re-fetch");
                    return GetRaw(symbol, start, end);
                }
                else
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return csvData;

        }

        /// <summary>
        /// Parse raw historical price data into list
        /// </summary>
        /// <param name="csvData"></param>
        /// <returns></returns>
        private static List<HistoryPrice> Parse(string csvData)
        {

            List<HistoryPrice> hps = new List<HistoryPrice>();

            try
            {
                string[] rows = csvData.Split(Convert.ToChar(10));

                //row(0) was ignored because is column names 
                //data is read from oldest to latest
                for (int i = 1; i <= rows.Length - 1; i++)
                {

                    string row = rows[i];
                    if (string.IsNullOrEmpty(row))
                        continue;

                    string[] cols = row.Split(',');
                    if (cols[1] == "null")
                        continue;

                    HistoryPrice hp = new HistoryPrice();
                    hp.Date = DateTime.Parse(cols[0]);
                    hp.Open = Convert.ToDouble(cols[1]);
                    hp.High = Convert.ToDouble(cols[2]);
                    hp.Low = Convert.ToDouble(cols[3]);
                    hp.Close = Convert.ToDouble(cols[4]);
                    hp.AdjClose = Convert.ToDouble(cols[5]);

                    //fixed issue in some currencies quote (e.g: SGDAUD=X)
                    if (cols[6] != "null")
                        hp.Volume = Convert.ToDouble(cols[6]);

                    hps.Add(hp);

                }

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return hps;

        }

        #region Unix Timestamp Converter

        //credits to ScottCher
        //reference http://stackoverflow.com/questions/249760/how-to-convert-a-unix-timestamp-to-datetime-and-vice-versa
        private static DateTime UnixTimestampToDateTime(double unixTimeStamp)
        {
            //Unix timestamp Is seconds past epoch
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
        }

        //credits to Dmitry Fedorkov
        //reference http://stackoverflow.com/questions/249760/how-to-convert-a-unix-timestamp-to-datetime-and-vice-versa
        private static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            //Unix timestamp Is seconds past epoch
            return (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        #endregion

    }

    public class HistoryPrice
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double AdjClose { get; set; }
    }
}