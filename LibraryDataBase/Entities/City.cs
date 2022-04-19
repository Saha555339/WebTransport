using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LibraryDataBase.Entities
{
    public class City: BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Name { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<Route> Routes { get; set; }
        public City()
        {

        }

    }
}
