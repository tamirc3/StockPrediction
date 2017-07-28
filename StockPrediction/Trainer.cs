using System.Collections;
using System.Collections.Generic;
using Accord.Math.Distances;
using Stocks;

namespace StockPrediction
{
    public class Trainer
    {
        public Model Train(IDataContainer dataContainer)
        {
            return null;
        }
    }

    public class Model
    {

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