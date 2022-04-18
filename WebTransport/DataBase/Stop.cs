using System.ComponentModel.DataAnnotations;

namespace WebTransport.DataBase
{
    public class Stop : BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Required]
        public int DistrictId { get; set; }
        public District District { get; set; }
        public Stop()
        {

        }
    }
}
