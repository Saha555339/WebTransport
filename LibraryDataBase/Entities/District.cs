using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LibraryDataBase.Entities
{
    public class District: BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City {get;set; }
        public ICollection<Stop> Stops { get; set; }
        public District()
        {

        }
    }
}
