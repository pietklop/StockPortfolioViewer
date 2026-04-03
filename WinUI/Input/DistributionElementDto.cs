namespace Dashboard.Input;

public class DistributionElementDto
{
    public string Key { get; }
    public double Fraction { get; private set; }
    public DistributionElementDto(string key, double fraction)
    {
        Key = key;
        Fraction = fraction;
    }

    public void UpdateValue(double fraction) => Fraction = fraction;
}