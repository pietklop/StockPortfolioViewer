using System.Collections.Generic;
using Imports;

namespace Messages.Dtos
{
    public class ImportDto
    {
        public List<TransactionDto> Transactions { get; }
        public List<DividendDto> Dividends { get; }

        public ImportDto(List<TransactionDto> transactions, List<DividendDto> dividends)
        {
            Transactions = transactions;
            Dividends = dividends;
        }
    }
}