using System;

namespace StockPrediction
{
    public class Stock
    {
        public Stock(double volume, double adjClose, double close, double low, double high, double open, DateTime date)
        {
            Volume = volume;
            AdjClose = adjClose;
            Close = close;
            Low = low;
            High = high;
            Open = open;
            Date = date;
        }

        public DateTime Date { get; set; }

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public double AdjClose { get; set; }

        public double Volume { get; set; }
    }
}