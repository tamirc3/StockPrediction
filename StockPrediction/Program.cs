using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvReader csvReader = new CsvReader();
            var x = csvReader.ReadFile(@"C:\Users\Tamir\Desktop\AMZN.csv");

            foreach (var variable in x)
            {
                Console.WriteLine(variable.Volume);
            }
        }
    }

    public class CsvReader
    {
        public IEnumerable<Stock> ReadFile(string fileName, string delimiter = ",")
        {
            List<Stock> res = new List<Stock>();

            var lines = File.ReadAllLines(fileName).Select(x => x.Split(new string[] { delimiter }, StringSplitOptions.None)).ToList();
            lines.RemoveAt(0);//remove the title line

            foreach (var splitedLine in lines)
            {
                res.Add(new Stock(Convert.ToDouble(splitedLine[6]), Convert.ToDouble(splitedLine[5]),
                    Convert.ToDouble(splitedLine[4]), Convert.ToDouble(splitedLine[3]),
                    Convert.ToDouble(splitedLine[2]), Convert.ToDouble(splitedLine[1]), Convert.ToDateTime(splitedLine[0])));
            }
            return res;
        }
    }

    public class Stock
    {
        public Stock(double volume, double adjClose, double close, double low, double high, double open, DateTime date)
        {
            this.Volume = volume;
            this.AdjClose = adjClose;
            this.Close = close;
            this.Low = low;
            this.High = high;
            this.Open = open;
            this.Date = date;
        }

        public DateTime Date { get; set; }

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public double AdjClose { get; set; }

        public double Volume { get; set; }
    }
}
