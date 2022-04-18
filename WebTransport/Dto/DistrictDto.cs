using System.Collections.Generic;

namespace WebTransport.Dto
{
    public class DistrictDto
    {
        public string Name { get; set; }
        public List<StopDto> Stops { get; set; }
        public DistrictDto(string name, List<StopDto> stops)
        {
            Name = name;
            Stops = stops;
        }

    }
}
