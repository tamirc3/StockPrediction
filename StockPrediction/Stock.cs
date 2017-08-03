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

        private bool Equals(Stock other)
        {
            return Date.Date.Equals(other.Date.Date) && Open.Equals(other.Open) && High.Equals(other.High) && Low.Equals(other.Low) && Close.Equals(other.Close) && AdjClose.Equals(other.AdjClose) && Volume.Equals(other.Volume);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Stock) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Date.GetHashCode();
                hashCode = (hashCode * 397) ^ Open.GetHashCode();
                hashCode = (hashCode * 397) ^ High.GetHashCode();
                hashCode = (hashCode * 397) ^ Low.GetHashCode();
                hashCode = (hashCode * 397) ^ Close.GetHashCode();
                hashCode = (hashCode * 397) ^ AdjClose.GetHashCode();
                hashCode = (hashCode * 397) ^ Volume.GetHashCode();
                return hashCode;
            }
        }
    }
}