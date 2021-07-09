using System.Collections.Generic;
using Imports;

namespace Messages.Dtos
{
    public class TransactionImportDto
    {
        public List<TransactionDto> Transactions { get; }
        public List<DividendDto> Dividends { get; }

        public TransactionImportDto(List<TransactionDto> transactions, List<DividendDto> dividends)
        {
            Transactions = transactions;
            Dividends = dividends;
        }
    }
}