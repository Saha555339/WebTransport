using System.Collections.Generic;
using WebTransport.DataBase;

namespace WebTransport.DataParse
{
    public class RouteFileParser : FileParser
    {
        public RouteFileParser(string path)
            : base(path)
        {
        }
        private List<RouteStops> _routeStops = new List<RouteStops>();
        public List<RouteStops> RouteStops
        {
            get { return _routeStops; }
        }

        public void ParseRoutes()
        {
            Parse();
            // Проверка на названия
            for (int i = 2; i < _arr.Length; i++)
            {
                string[] s = _arr[i].Split(";");
                RouteStops route = new RouteStops();
                s[1] = s[1].Replace("\"", "");
                route.Number = s[1];
                string[] trach_of_route = s[3].Split(" - ");
                for (int j = 0; j < trach_of_route.Length; j++)
                {
                    trach_of_route[j] = trach_of_route[j].Replace("\"", "");
                    route.Stops.Add(trach_of_route[j]);
                }
                _routeStops.Add(route);
            }
        }


    }
}
