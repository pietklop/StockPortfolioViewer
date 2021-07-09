using log4net;
using Messages.Dtos;

namespace Imports.DeGiro
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
                    return ImportType.Transaction;
                case "Product,Symbool/ISIN,Aantal,Slotkoers,Lokale waarde,Waarde in EUR":
                    return ImportType.StockValue;
                default:
                    return ImportType.Unknown;
            }
        }

        public TransactionImportDto TransactionImport(string[] lines, bool debugMode) => TransactionImporter.Import(lines, debugMode);

        public StockValueImportDto StockValueImport(string[] lines) => StockValueImporter.Import(lines);
    }
}