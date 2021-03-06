﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockPrediction
{
    public interface IDataContainer
    {
        IList GetData(string symbol);
        List<string> DataSymbols { get; }
    }

    public  class DataContainer : IDataContainer
    {
        private Dictionary<string, IList> SymbolDataDictionary { get; set; }
        private readonly List<string> symbols;
        private readonly IDataBringer dataBringer;

        public List<string> DataSymbols => symbols;

        public  DataContainer(List<string> symbols, IDataBringer dataBringer)
        {
            this.symbols = symbols;
            this.dataBringer = dataBringer;
            SymbolDataDictionary = new Dictionary<string, IList>();
        }

        public void GetDataForSymbols()
        {
            GetDataFroAllSymbols();
        }

        private void GetDataFroAllSymbols()
        {
            foreach (var synbol in symbols)
            {
                var data = dataBringer.BringMeData(synbol);
                SymbolDataDictionary.Add(synbol, data);
            }
        }

        public Task GetDataFromSymbolsAsync()
        {
            return Task.Factory.StartNew(GetDataFroAllSymbols);
        }
        public IList GetData(string symbol)
        {
            return SymbolDataDictionary[symbol];
        }

    }
}