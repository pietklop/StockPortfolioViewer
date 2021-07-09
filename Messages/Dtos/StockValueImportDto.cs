using System.Collections.Generic;

namespace Messages.Dtos
{
    public class StockValueImportDto
    {
        public List<StockValueDto> StockValues { get; }

        public StockValueImportDto(List<StockValueDto> stockValues)
        {
            StockValues = stockValues;
        }
    }
}