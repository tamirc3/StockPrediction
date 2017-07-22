using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;


namespace Stocks
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvReader csvReader = new CsvReader();
            var listOfStocks = csvReader.ReadFile(@"C:\Users\Tamir\Desktop\AMZN.csv");

            // The multivariate linear regression is a generalization of
            // the multiple linear regression. In the multivariate linear
            // regression, not only the input variables are multivariate,
            // but also are the output dependent variables.

            // In the following example, we will perform a regression of
            // a 2-dimensional output variable over a 3-dimensional input
            // variable.

            var inputs = new double[listOfStocks.Count()][];
            var outputs = new double[listOfStocks.Count()][];
            for (int i = 0; i < listOfStocks.Count(); i++)
            {
                inputs[i] = new double[]
                {
                    listOfStocks[i].AdjClose,
                    listOfStocks[i].Close,
                    listOfStocks[i].High,
                    listOfStocks[i].Low,
                    listOfStocks[i].Open,
                };
                outputs[i] = new double[]{listOfStocks[i].Volume};
            }

           

            // With a quick eye inspection, it is possible to see that
            // the first output variable y1 is always the double of the
            // first input variable. The second output variable y2 is
            // always the triple of the first input variable. The other
            // input variables are unused. Nevertheless, we will fit a
            // multivariate regression model and confirm the validity
            // of our impressions:

            // Use Ordinary Least Squares to create the regression
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();

            // Now, compute the multivariate linear regression:
            MultivariateLinearRegression regression = ols.Learn(inputs, outputs);

            // We can obtain predictions using
            double[][] predictions = regression.Transform(inputs);

            // The prediction error is
            double error = new SquareLoss(outputs).Loss(predictions); // 0

            // At this point, the regression error will be 0 (the fit was
            // perfect). The regression coefficients for the first input
            // and first output variables will be 2. The coefficient for
            // the first input and second output variables will be 3. All
            // others will be 0.
            // 
            // regression.Coefficients should be the matrix given by
            // 
            // double[,] coefficients = {
            //                              { 2, 3 },
            //                              { 0, 0 },
            //                              { 0, 0 },
            //                          };
            // 

            // We can also check the r-squared coefficients of determination:
            double[] r2 = regression.CoefficientOfDetermination(inputs, outputs);


            foreach (var row in regression.Weights)
            {
                foreach (var d in row)
                {
                    Console.Write(d);
                }
                Console.WriteLine();
            }
        }
    }

    public class CsvReader
    {
        public List<Stock> ReadFile(string fileName, string delimiter = ",")
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
