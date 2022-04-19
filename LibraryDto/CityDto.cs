using System.Collections.Generic;

namespace LibraryDto
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DistrictDto> Districts { get; set; }
        public CityDto()
        {

        }
        public CityDto(string name, List<DistrictDto> districts)
        {
            Name = name;
            Districts = districts;
        }
    }
}
