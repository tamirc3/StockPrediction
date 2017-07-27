using StockPrediction;

namespace Stocks
{
    class Program
    {
        static void Main(string[] args)
        {

            var wrapper = new YahooWrapper();
            var listOfStocks = wrapper.GetShitFromYahoo("AMZN");
             Predictor predictor = new Predictor();
            predictor.Predict(listOfStocks);
        }
    }
}
