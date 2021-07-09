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

        public TransactionImportDto TransactionImport(string[] lines, bool debugMode) => TransactionImporter.Import(lines, debugMode);
    }
}