namespace Dashboard.Input
{
    public class AreaCountryInputDto
    {
        public string Continent { get; }
        public string Country { get; }

        public AreaCountryInputDto(string continent, string country)
        {
            Continent = continent;
            Country = country;
        }
    }
}