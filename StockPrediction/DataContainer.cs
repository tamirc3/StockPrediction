using System.Collections;
using System.Collections.Generic;
using Stocks;

namespace StockPrediction
{
    public interface IDataContainer
    {
        IList GetData();
        List<string> Symbols { get; set; }
    }

    public class YahooDataContainer: IDataContainer
    {
        public List<string> Symbols { get; set; }

        public IList GetData()
        {
            var wrapper = new YahooWrapper();
            var listOfStocks = wrapper.BringMeData("AMZN");
            return listOfStocks;
        }
    }

    public class CsvDataContainer : IDataContainer
    {

        public IList GetData()
        {
            CsvReader csvReader = new CsvReader();
            return csvReader.BringMeData("AMZN");
        }

        public List<string> Symbols { get; set; }
    }
}