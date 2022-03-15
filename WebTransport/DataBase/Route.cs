using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTransport.DataBase
{
    public class Route
    {
        [Required]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Stop> Stops = new List<Stop>();
    }
}
