using System.Collections.Generic;

namespace LibraryDto
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StopDto> Stops { get; set; }
        public DistrictDto(string name, List<StopDto> stops)
        {
            Name = name;
            Stops = stops;
        }

    }
}
