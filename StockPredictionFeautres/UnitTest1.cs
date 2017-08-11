using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockPredictionFeautres
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /*
              var csvFile = $@"h1, h2, h3,h4,h5,h6,h7{Environment.NewLine}{DateTime.MaxValue},1.0,1.0,1.0,1.0,1.0,1.0";
            List<Stock> expectedList = new List<Stock> {new Stock(1.0, 1.0, 1.0, 1.0, 1.0, 1.0, DateTime.MaxValue)};
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            File.WriteAllText(fileNameWithoutExtension+".csv", csvFile);
            CsvReader csvReader = new CsvReader();
            var data = csvReader.BringMeData(fileNameWithoutExtension);
            Assert.AreEqual(expectedList, data);
            if(File.Exists(fileNameWithoutExtension + ".csv"))
                File.Delete(fileNameWithoutExtension+".csv"); 
              
              
              
              */
        }
    }
}
