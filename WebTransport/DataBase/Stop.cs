using System.ComponentModel.DataAnnotations;

namespace WebTransport.DataBase
{
    public class Stop
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public District District { get; set; }
        [Required]
        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}
