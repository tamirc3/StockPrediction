using System.Collections;
using System.Collections.Generic;

namespace StockPrediction
{
    public interface IDataContainer
    {
        IList GetData(string symbol);
        List<string> DataSymbols { get; }
    }

    public abstract class BaseDataContainer : IDataContainer
    {
        private Dictionary<string, IList> SymbolDataDictionary { get; set; }
        private readonly List<string> symbols;
        private readonly IDataBringer dataBringer;

        protected BaseDataContainer(List<string> symbols, IDataBringer dataBringer)
        {
            this.symbols = symbols;
            this.dataBringer = dataBringer;
            SymbolDataDictionary = new Dictionary<string, IList>();
            InitData();
        }

        private void InitData()
        {
            foreach (var synbol in symbols)
            {
                var data = dataBringer.BringMeData(synbol);
                SymbolDataDictionary.Add(synbol, data);
            }
        }

        public IList GetData(string symbol)
        {
            return SymbolDataDictionary[symbol];
        }


        public List<string> DataSymbols => symbols;
    }

    public class YahooDataContainer: BaseDataContainer
    {
        public YahooDataContainer(List<string> symbols, IDataBringer dataBringer) : base(symbols, dataBringer)
        {

        }

    }

    public class CsvDataContainer : BaseDataContainer
    {
        public CsvDataContainer(List<string> symbols, IDataBringer dataBringer) : base(symbols, dataBringer)
        {
        }
    }
}