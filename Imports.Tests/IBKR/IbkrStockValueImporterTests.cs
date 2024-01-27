using Imports.IBKR;
using Shouldly;
using Xunit;

namespace Imports.Tests.IBKR
{
    public class IbkrStockValueImporterTests
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var txt = "CurrencyPrimary|Symbol|Description|ISIN|ReportDate|Quantity|PositionValue|MarkPrice\r\n" +
                      "USD|BYDDY|BYD CO LTD|US05606L1008|20240112|115|6196.2|55.5\r\n" +
                      "EUR|ASR.AMS|ASR|NL123456|20240112|180|1731.6|44\r\n" +
                      // currencies
                      "Date/Time|FromCurrency|ToCurrency|Rate\r\n" +
                      "20240112|CHF|EUR|1.0711\r\n" +
                      "20240112|USD|EUR|0.9000\r\n" +
                      "20240112|MXN|EUR|0.054143\r\n" +
                      "20240112|SAR|EUR|0.24346";
            var lines = txt.Split("\r\n");

            // Act
            var import = IbkrStockValueImporter.Import(lines);

            // Assert
            import.StockValues.Count.ShouldBe(2);
            var byd = import.StockValues[0];
            byd.Name.ShouldBe("BYD CO LTD");
            byd.Isin.ShouldBe("US05606L1008");
            byd.Currency.ShouldBe("USD");
            byd.CurrencyRatio.ShouldBe(0.9);
            byd.TimeStamp.Day.ShouldBe(12);
            byd.TimeStamp.Month.ShouldBe(1);
            byd.TimeStamp.Year.ShouldBe(2024);
            byd.ClosePrice.ShouldBe(55.5);
            byd.Quantity.ShouldBe(115);

            var asr = import.StockValues[1];
            asr.Name.ShouldBe("ASR");
            asr.Isin.ShouldBe("NL123456");
            asr.Currency.ShouldBe("EUR");
            asr.CurrencyRatio.ShouldBe(1);
            asr.TimeStamp.Day.ShouldBe(12);
            asr.TimeStamp.Month.ShouldBe(1);
            asr.TimeStamp.Year.ShouldBe(2024);
            asr.ClosePrice.ShouldBe(44);
            asr.Quantity.ShouldBe(180);
        }

    }
}