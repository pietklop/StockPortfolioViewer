using Dashboard.Input;
using Messages.UI;

namespace Dashboard.StockDetails;

public class DistributionViewModel
{
    [ColumnCellsUnderline]
    public string Key { get; }
    [ColumnCellsUnderline]
    [DisplayFormat("P0")]
    public double Fraction { get; }

    public DistributionViewModel(DistributionElementDto element)
    {
        Key = element.Key;
        Fraction = element.Fraction;
    }
}