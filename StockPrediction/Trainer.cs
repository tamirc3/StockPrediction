using System.Collections;
using System.Collections.Generic;
using Accord.Math.Distances;
using Stocks;

namespace StockPrediction
{
    public class Trainer
    {
        public Model Train(DataContainer dataContainer)
        {
            return null;
        }
    }

    public class Model
    {

    }

    

    public class DataContainer
    {

        public IList GetData()
        {

            var wrapper = new YahooWrapper();
            var listOfStocks = wrapper.BringMeData("AMZN");
            return listOfStocks;
        }
    }

    public class Evaluator
    {
        public double Evalutate()
        {
            return 0.0;
        }
    }

    public interface IDataBringer
    {
        IList BringMeData(string symbol);

    }





}