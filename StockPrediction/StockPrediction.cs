using System;
using System.Collections.Generic;
using Stocks;

namespace StockPrediction
{
    public class StockPrediction
    {
   

        public StockPrediction()
        {
       
        }

        public void GetAndTrainYahoo()
        {

            string amzn = "AMZN";
            var yahooSymbols = new List<string>()
            {
                amzn
            };

            YahooDataContainer yahooDataContainer = new YahooDataContainer(yahooSymbols,new YahooDataBringer() );


            Trainer  trainer =new Trainer();

           var model =  trainer.Train(yahooDataContainer);

            var weights = model.SynbolWeightDictionary[amzn];

            foreach (var row in weights)
            {
                foreach (var d in row)
                {
                    Console.Write(d);
                }
                Console.WriteLine();
            }

            Evaluator evaluator = new Evaluator();
            evaluator.Evalutate(model);
        }
    }
}