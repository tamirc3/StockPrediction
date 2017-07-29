using System.Collections;

namespace StockPrediction
{
    public interface IDataBringer
    {
        IList BringMeData(string symbol);
    }
}