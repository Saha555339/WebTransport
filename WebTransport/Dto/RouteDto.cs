using System.Collections.Generic;

namespace WebTransport.Dto
{
    public class RouteDto
    {
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
