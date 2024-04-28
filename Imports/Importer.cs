using Imports.DeGiro;
using Imports.IBKR;
using log4net;
using Messages.Dtos;

namespace Imports
{
    public class Importer
    {
        private readonly ILog log;

        public Importer(ILog log)
        {
            this.log = log;
        }

        public ImportType DetermineImportType(string[] lines)
        {
            var header = lines[0];
            switch (header)
            {
                case "Datum,Tijd,Valutadatum,Product,ISIN,Omschrijving,FX,Mutatie,,Saldo,,Order Id":
                    return ImportType.DeGiroTransactionsAndDividends;
                case "Product,Symbool/ISIN,Aantal,Slotkoers,Lokale waarde,Waarde in EUR":
                    return ImportType.DeGiroStockValues;
                case "CurrencyPrimary|Symbol|Description|ISIN|ExDate|PayDate|Quantity|Tax|Fee|GrossRate|GrossAmount|NetAmount|ActionID|Code":
                    return ImportType.IbkrDividend;
                case "Description|Symbol|ISIN|CurrencyPrimary|TradePrice|TradeMoney|Quantity|TradeDate|AssetClass|Buy/Sell|IBCommissionCurrency|IBCommission|TradeID":
                    return ImportType.IbkrTransactions;
                case "CurrencyPrimary|Symbol|Description|ISIN|ReportDate|Quantity|PositionValue|MarkPrice":
                    return ImportType.IbkrStockValues;
                default:
                    return ImportType.Unknown;
            }
        }

        public TransactionImportDto DeGiroTransactionAndDividendImport(string[] lines, bool debugMode) => TransactionImporter.Import(lines, debugMode);

        public StockValueImportDto DeGiroStockValueImport(string[] lines) => StockValueImporter.Import(lines);

        public TransactionImportDto IbkrDividendImport(string[] lines) => DividendImporter.Import(lines);

        public TransactionImportDto IbkrTransactionImport(string[] lines, bool debugMode) => TradeImporter.Import(lines);

        public StockValueImportDto IbkrStockValueImport(string[] lines) => IbkrStockValueImporter.Import(lines);
    }
}