using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Currency
    {
        [StringLength(3)]
        public string Key { get; set; }
        public string Symbol { get; set; }
    }
}