using System;
using System.Collections.Generic;

namespace LibraryDto
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StopsCount { get; set; }
        private List<Tuple<int, int>> _routePairsId = new();
        public List<Tuple<int, int>> RoutePairsId
        {
            get { return _routePairsId; }
            set { _routePairsId = value; }
        }
        public int RoutePairsCount { get; set; }
        public DistrictDto()
        {

        }
        public DistrictDto(string name, int stopsCount, int routePairsCount)
        {
            Name = name;
            StopsCount = stopsCount;
            RoutePairsCount = routePairsCount;
        }
    }
}
