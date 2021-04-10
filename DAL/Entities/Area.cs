using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace DAL.Entities
{
    public class Area : Entity
    {
        [Required] public string Name { get; set; }
        /// <summary>
        /// Can be null in case it is a continent
        /// </summary>
        [CanBeNull] public Area Continent { get; set; }
        public bool IsContinent { get; set; }
    }

    public class AreaShare : Entity
    {
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public double Fraction { get; set; }
    }

}