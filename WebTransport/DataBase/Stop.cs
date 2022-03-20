using System.ComponentModel.DataAnnotations;

namespace WebTransport.DataBase
{
    public class Stop: BaseEntity
    {
        [Required]
        public override int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //[Required]
        //public int DistrictId { get; set; }
        //public District District { get; set; }
        [Required]
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public Stop()
        {

        }
        public Stop(string name, double latitude, double longitude, /*int districtId, District district,*/ int routeId, Route route, int id=0)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            //DistrictId = districtId;
            //District = district;
            RouteId = routeId;
            Route = route;
        }
    }
}
