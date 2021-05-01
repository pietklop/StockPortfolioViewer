using System.Collections.Generic;

namespace Dashboard.Input
{
    public class DistributionInputDto
    {
        public string[] Keys { get; }
        public double[] Fractions { get; }
        public List<AreaCountryInputDto> NewCountries { get; }

        public DistributionInputDto(string[] keys, double[] fractions, List<AreaCountryInputDto> newCountries = null)
        {
            Keys = keys;
            Fractions = fractions;
            NewCountries = newCountries;
        }
    }
}