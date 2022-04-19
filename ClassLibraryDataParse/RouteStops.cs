using System.Collections.Generic;

namespace LibraryDataParse
{
    public class RouteStops
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public List<string> Stops = new List<string>();
    }
}
