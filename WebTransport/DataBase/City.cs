using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTransport.DataBase
{
    public class City: BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Name { get; set; }
        public List<District> Districts = new List<District>();
        public City()
        {

        }
        public City(string name, List<District> districts, int id=0)
        {
            Id = id;
            Name = name;
            Districts = districts;
        }

    }
}
