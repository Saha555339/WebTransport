using System.Collections.Generic;

namespace WebTransport.Dto
{
    public class StopDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Routes = new List<string>();
        public StopDto(string name, double latitude, double longitude, List<string> routes)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Routes = routes;
        }
    }
}
