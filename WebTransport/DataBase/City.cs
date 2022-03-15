using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTransport.DataBase
{
    public class City
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<District> Districts = new List<District>();

    }
}
