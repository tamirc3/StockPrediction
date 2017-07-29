using System.Collections.Generic;

namespace StockPrediction
{
    public class Model
    {
        //todo decide what will the model have


        public Dictionary<string, double[][]> SynbolWeightDictionary { get; set; }

        public Model()
        {
            SynbolWeightDictionary =new Dictionary<string, double[][]>();
        }

        public void AddSymbolAndWeight(string symbol, double[][] weightsList)
        {
            SynbolWeightDictionary.Add(symbol,weightsList);
        }
    }
}