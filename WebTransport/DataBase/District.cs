using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTransport.DataBase
{
    public class District: BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        public District()
        {

        }

        public District(string name, int cityId, City city, int id=0)
        {
            Id = id;
            Name = name;
            CityId = cityId;
            City = city;
        }

    }
}
