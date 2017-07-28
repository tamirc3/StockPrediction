using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StockPrediction
{
    public class CsvReader : IDataBringer
    {
        private List<Stock> ReadFile(string fileName, string delimiter = ",")
        {
            List<Stock> res = new List<Stock>();

            var lines = File.ReadAllLines(fileName)
                .Select(x => x.Split(new[] {delimiter}, StringSplitOptions.None)).ToList();
            lines.RemoveAt(0); //remove the title line

            foreach (var splitedLine in lines)
            {
                res.Add(new Stock(Convert.ToDouble(splitedLine[6]), Convert.ToDouble(splitedLine[5]),
                    Convert.ToDouble(splitedLine[4]), Convert.ToDouble(splitedLine[3]),
                    Convert.ToDouble(splitedLine[2]), Convert.ToDouble(splitedLine[1]),
                    Convert.ToDateTime(splitedLine[0])));
            }
            return res;
        }

        public IList BringMeData(string symbol)
        {
            return ReadFile(symbol + ".csv");
        }
    }
}