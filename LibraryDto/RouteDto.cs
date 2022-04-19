using System.Collections.Generic;

namespace LibraryDto
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public List<StopDto> Stops { get; set; }
        public RouteDto()
        {

        }
        public RouteDto(string number, string type, List<StopDto> stops)
        {
            Number = number;
            Type = type;
            Stops = stops;
        }
    }
}
