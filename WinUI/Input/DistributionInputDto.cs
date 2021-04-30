namespace Dashboard.Input
{
    public class DistributionInputDto
    {
        public string[] Keys { get; }
        public double[] Fractions { get; }

        public DistributionInputDto(string[] keys, double[] fractions)
        {
            Keys = keys;
            Fractions = fractions;
        }
    }
}