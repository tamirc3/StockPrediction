namespace StockPrediction
{
    class Program
    {
        static void Main()
        {

         StockPrediction prediction =new StockPrediction(new Trainer(), new Evaluator());
            prediction.GetAndTrainYahoo();

        }
    }
}
