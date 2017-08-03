using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.Math;

namespace StockPrediction
{
    public class CsvReader : IDataBringer
    {
        private List<Stock> ReadFile(string fileName, string delimiter = ",")
        {
            List<Stock> res = new List<Stock>();

            var lines = File.ReadAllLines(fileName)
                .Select(x =>
                {
                    return x.Split(new[] {delimiter}, StringSplitOptions.None).Select(y => y.Trim()).ToList();
                }).ToList();
            lines.RemoveAt(0); //remove the title line

            foreach (var splitedLine in lines)
            {
                res.Add(new Stock(double.Parse(splitedLine[6]), double.Parse(splitedLine[5]),
                    double.Parse(splitedLine[4]), double.Parse(splitedLine[3]),
                    double.Parse(splitedLine[2]), double.Parse(splitedLine[1]),
                    DateTime.Parse(splitedLine[0])));
            }
            return res;
        }

        public IList BringMeData(string symbol)
        {
            return ReadFile(symbol + ".csv");
        }
    }
}