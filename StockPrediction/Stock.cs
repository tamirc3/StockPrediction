using System;

namespace Stocks
{
    public class Stock
    {
        public Stock(double volume, double adjClose, double close, double low, double high, double open, DateTime date)
        {
            this.Volume = volume;
            this.AdjClose = adjClose;
            this.Close = close;
            this.Low = low;
            this.High = high;
            this.Open = open;
            this.Date = date;

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