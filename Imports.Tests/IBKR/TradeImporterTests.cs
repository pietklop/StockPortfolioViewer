using System;
using Imports.IBKR;
using Shouldly;
using Xunit;

namespace Imports.Tests.IBKR
{
    public class TradeImporterTests
    {
        // buy / sell

        [Fact]
        public void Test_Simple_Trade()
        {
            // Arrange
            var txt = "Description|Symbol|ISIN|CurrencyPrimary|TradePrice|TradeMoney|Quantity|TradeDate|AssetClass|Buy/Sell|IBCommissionCurrency|IBCommission|TradeID\r\n" +
                      "BYD CO LTD|BYDDY|US05606L1008|USD|52.74|6065.1|115|20240108|STK|BUY|USD|-1|\r\n" +
                      "BYD CO LTD|BYDDY|US05606L1008|USD|52.74|6065.1|115|20240108|STK|BUY|USD|-1|\r\n" +
                      "BYD CO LTD|BYDDY|US05606L1008|USD|52.74|6065.1|115|20240108|STK|BUY|USD|-1|663401278\r\n" +
                      "EUR.USD|EUR.USD||USD|1.0956|-8819.58|-8050|20240108|CASH|SELL|EUR|-1.82788|663377523\r\n" + // should ignore
                      // currencies
                      "Date/Time|FromCurrency|ToCurrency|Rate\r\n" +
                      "20240104|USD|EUR|0.91365\r\n" +
                      "20240105|USD|EUR|0.91394\r\n" +
                      "20240107|USD|EUR|0.91391\r\n" +
                      "20240108|USD|EUR|0.90000\r\n" +
                      "20240109|USD|EUR|0.9148\r\n" +
                      "20240110|USD|EUR|0.91134";
            var lines = txt.Split("\r\n");

            // Act
            var import = TradeImporter.Import(lines);

            // Assert
            import.Dividends.Count.ShouldBe(0);
            import.Transactions.Count.ShouldBe(1);
            var transaction = import.Transactions[0];
            transaction.Name.ShouldBe("BYD CO LTD");
            transaction.Isin.ShouldBe("US05606L1008");
            transaction.Currency.ShouldBe("USD");
            transaction.TimeStamp.Day.ShouldBe(8);
            transaction.TimeStamp.Month.ShouldBe(1);
            transaction.TimeStamp.Year.ShouldBe(2024);
            transaction.Quantity.ShouldBe(115d);
            transaction.Price.ShouldBe(52.74);
            transaction.Costs.ShouldBe(0.9);
            transaction.CurrencyRatio.ShouldBe(0.9);
            transaction.Guid.ShouldBe("663401278");
        }

        [Fact]
        public void Test_split_order()
        {
            // Arrange
            var txt = "Description|Symbol|ISIN|CurrencyPrimary|TradePrice|TradeMoney|Quantity|TradeDate|AssetClass|Buy/Sell|IBCommissionCurrency|IBCommission|TradeID\r\n" +
                      "MAGIC|MGIC|IL0010823123|USD|9.584444444|1725.2|180|20240108|STK|BUY|USD|-1|\r\n" + // to be ignored, no tradeId
                      "MAGIC|MGIC|IL0010823123|USD|6|767.2|300|20240108|STK|BUY|USD|-1|663413170\r\n" +
                      "MAGIC|MGIC|IL0010823123|USD|10|958|100|20240108|STK|BUY|USD|0|663413221\r\n" +
                      // currencies
                      "Date/Time|FromCurrency|ToCurrency|Rate\r\n" +
                      "20240107|CHF|EUR|1.0485\r\n" +
                      "20240108|CHF|EUR|1.0548\r\n" +
                      "20240104|USD|EUR|0.91365\r\n" +
                      "20240105|USD|EUR|0.91394\r\n" +
                      "20240107|USD|EUR|0.91391\r\n" +
                      "20240108|USD|EUR|0.90000\r\n" +
                      "20240109|USD|EUR|0.9148\r\n" +
                      "20240110|USD|EUR|0.91134";
            var lines = txt.Split("\r\n");

            // Act
            var import = TradeImporter.Import(lines);

            // Assert
            import.Dividends.Count.ShouldBe(0);
            import.Transactions.Count.ShouldBe(1);
            var transaction = import.Transactions[0];
            transaction.Name.ShouldBe("MAGIC");
            transaction.Isin.ShouldBe("IL0010823123");
            transaction.Currency.ShouldBe("USD");
            transaction.Quantity.ShouldBe(400d);
            transaction.Price.ShouldBe(7); // weighted average
            transaction.Costs.ShouldBe(0.9);
        }

        [Fact]
        public void Test_multiple_trades_different_dates()
        {
            // Arrange
            var txt = "Description|Symbol|ISIN|CurrencyPrimary|TradePrice|TradeMoney|Quantity|TradeDate|AssetClass|Buy/Sell|IBCommissionCurrency|IBCommission|TradeID\r\n" +
                      "MAGIC|MGIC|IL0010823123|USD|9.584444444|1725.2|180|20240108|STK|BUY|USD|-1|\r\n" + // to be ignored, no tradeId
                      "MAGIC|MGIC|IL0010823123|USD|10|767.2|40|20240108|STK|BUY|USD|-1|663413170\r\n" +
                      "MAGIC|MGIC|IL0010823123|USD|10|958|60|20240108|STK|BUY|USD|0|663413221\r\n" +
                      "MAGIC|MGIC|IL0010823123|USD|11|958|200|20240110|STK|BUY|USD|-2|66341399\r\n" +
                      // currencies
                      "Date/Time|FromCurrency|ToCurrency|Rate\r\n" +
                      "20240107|CHF|EUR|1.0485\r\n" +
                      "20240108|CHF|EUR|1.0548\r\n" +
                      "20240104|USD|EUR|0.91365\r\n" +
                      "20240105|USD|EUR|0.91394\r\n" +
                      "20240107|USD|EUR|0.91391\r\n" +
                      "20240108|USD|EUR|0.90000\r\n" +
                      "20240109|USD|EUR|0.9148\r\n" +
                      "20240110|USD|EUR|0.95000";
            var lines = txt.Split("\r\n");

            // Act
            var import = TradeImporter.Import(lines);

            // Assert
            import.Dividends.Count.ShouldBe(0);
            import.Transactions.Count.ShouldBe(2);
            var transaction1 = import.Transactions[0];
            transaction1.Isin.ShouldBe("IL0010823123");
            transaction1.Quantity.ShouldBe(100d);
            transaction1.Price.ShouldBe(10); // weighted average
            transaction1.Costs.ShouldBe(0.9);
            transaction1.CurrencyRatio.ShouldBe(0.9);
            var transaction2 = import.Transactions[1];
            transaction2.Isin.ShouldBe("IL0010823123");
            transaction2.Quantity.ShouldBe(200d);
            transaction2.Price.ShouldBe(11); // weighted average
            transaction2.Costs.ShouldBe(1.9);
            transaction2.CurrencyRatio.ShouldBe(0.95);
        }

        [Fact]
        public void Test_missing_currency()
        {
            // Arrange
            var txt = "Description|Symbol|ISIN|CurrencyPrimary|TradePrice|TradeMoney|Quantity|TradeDate|AssetClass|Buy/Sell|IBCommissionCurrency|IBCommission|TradeID\r\n" +
                      "MAGIC|MGIC|IL0010823123|USD|10|958|100|20241111|STK|BUY|USD|0|663413221\r\n" +
                      // currencies
                      "Date/Time|FromCurrency|ToCurrency|Rate\r\n" +
                      "20240107|CHF|EUR|1.0485\r\n" +
                      "20240108|CHF|EUR|1.0548\r\n" +
                      "20240104|USD|EUR|0.91365\r\n" +
                      "20240105|USD|EUR|0.91394\r\n" +
                      "20240107|USD|EUR|0.91391\r\n" +
                      "20240108|USD|EUR|0.90000\r\n" +
                      "20240109|USD|EUR|0.9148\r\n" +
                      "20240110|USD|EUR|0.91134";
            var lines = txt.Split("\r\n");

            // Act & Assert
            var ex = Should.Throw<Exception>(() => TradeImporter.Import(lines));
            ex.Message.ShouldStartWith("Could not find matching currency");
        }
    }
}