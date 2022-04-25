using System.Collections.Generic;

namespace LibraryDto
{
    public class StopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public List<string> Routes { get; set; }
        public int DistrictId { get; set; }
        public string District { get; set; }
        public StopDto()
        {

        }
        public StopDto(string name, double latitude, double longitude, List<string> route, string district)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            //Routes = route;
            District = district;
        }
    }
}
