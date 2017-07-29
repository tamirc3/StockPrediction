using System.Diagnostics;

namespace StockPrediction
{
    class Program
    {
        static void Main()
        {
           // Debug.Listeners.Add(new TextWriterTraceListener(@"debug.log"));
           // Debug.AutoFlush = true;
          //  Debug.WriteLine("hello");
         
           StockPrediction prediction =new StockPrediction(new Trainer(), new Evaluator());
            prediction.GetAndTrainYahoo();

           // Debug.Close();
        }
    }
}
