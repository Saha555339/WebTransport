using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTransport.DataBase
{
    public class District
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Stop> Stops = new List<Stop>();

    }
}
