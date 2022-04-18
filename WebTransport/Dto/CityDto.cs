using System.Collections.Generic;

namespace WebTransport.Dto
{
    public class CityDto
    {
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
