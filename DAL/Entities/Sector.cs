using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Sector : Entity
    {
        [Required] public string Name { get; set; }
    }

    public class SectorShare : Entity
    {
        public int SectorId { get; set; }
        public Sector Sector { get; set; }
        /// <summary>
        /// Max value is 1 (100%)
        /// </summary>
        public double Fraction { get; set; }
    }

}