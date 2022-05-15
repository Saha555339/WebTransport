using LibraryDto;
using System.Collections.Generic;

namespace LibraryLogic
{
    public class PairRoutes
    {
        public RouteDto FirstRoute { get; set; }
        public RouteDto SecondRoute { get; set; }
        public List<StopDto> Stops { get; set; }
        public double MatchPercentage { get; set; }
    }
}
