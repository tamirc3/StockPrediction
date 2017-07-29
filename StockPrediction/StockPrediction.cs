using System;
using System.Collections.Generic;
using Stocks;

namespace StockPrediction
{
    public class StockPrediction
    {
        private readonly Trainer trainer;
        private readonly Evaluator evaluator;

        public StockPrediction(Trainer trainer, Evaluator evaluator)
        {
            this.trainer = trainer;
            this.evaluator = evaluator;
        }

        public void GetAndTrainYahoo()
        {

            string amzn = "AMZN";
            var yahooSymbols = new List<string>()
            {
                amzn
            };

            DataContainer yahooDataContainer = new DataContainer(yahooSymbols,new YahooDataBringer() );


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

            evaluator.Evalutate(model);
        }
    }
}