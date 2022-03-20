using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTransport.DataBase
{
    public class Route: BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Stop> Stops = new List<Stop>();
        public Route()
        {

        }

        public Route(string number, string type, int cityId, City city, List<Stop> stops, int id=0)
        {
            Id = id;
            Number = number;
            Type = type;
            CityId = cityId;
            City = city;
            Stops = stops;
        }
    }
}
