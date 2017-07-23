using StockPrediction;

namespace Stocks
{
    class Program
    {
        static void Main(string[] args)
        {

            var wrapper = new YahooWrapper();
            var listOfStocks = wrapper.GetShitFromYahoo("AMZN");//csvReader.ReadFile(@"C:\Users\Tamir\Desktop\AMZN.csv");
             Predictor predictor = new Predictor();
            predictor.Predict(listOfStocks);

        }
    }
}
