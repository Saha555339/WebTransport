using System.Collections.Generic;

namespace WebTransport.Dto
{
    public class StopDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Route { get; set; }
        public string District { get; set; }
        public StopDto()
        {

        }
        public StopDto(string name, double latitude, double longitude, List<string> route, string district)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Route = route;
            District = district;
        }
    }
}
