using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDataBase.Entities;
using LibraryDataBase.DataLoading;

namespace DataCleaning
{
    public class NormalizationAlgorithm
    {

        TransportContext _transportContext;
        CreateAction<Route> _createRoutes;
        public NormalizationAlgorithm(TransportContext transportContext)
        {
            _transportContext = transportContext;
            _createRoutes = new CreateAction<Route>(_transportContext);
        }
        private List<Route> new_routes = new();

        private static double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            const int earth_radius = 6372795;
            double distance = 0;
            double lat1 = latitude1 * Math.PI / 180;
            double long1 = longitude1 * Math.PI / 180;
            double lat2 = latitude2 * Math.PI / 180;
            double long2 = longitude2 * Math.PI / 180;

            double cl1 = Math.Cos(lat1);
            double cl2 = Math.Cos(lat2);
            double sl1 = Math.Sin(lat1);
            double sl2 = Math.Sin(lat2);
            double delta = long2 - long1;
            double cdelta = Math.Cos(delta);
            double sdelta = Math.Sin(delta);

            double y = Math.Sqrt(Math.Pow((cl2 * sdelta), 2) + Math.Pow((cl1 * sl2 - sl1 * cl2 * cdelta), 2));
            double x = sl1 * sl2 + cl1 * cl2 * cdelta;

            double ad = Math.Atan2(y, x);
            distance = ad * earth_radius;

            return distance;
        }

        private static List<int> SearchWay(List<(double, double)> stopCoordinates)
        {
            List<int> _numbersOfStops = new();
            _numbersOfStops.Add(0);
            int k = 0;
            int kol = 1;
            while (kol < stopCoordinates.Count)
            {
                List<(double, int)> distance = new();
                for (int i = 1; i < stopCoordinates.Count; i++)
                {
                    if (_numbersOfStops.IndexOf(i) == -1)
                        distance.Add(new(CalculateDistance(stopCoordinates[k].Item1, stopCoordinates[k].Item2, stopCoordinates[i].Item1, stopCoordinates[i].Item2), i));
                }
                distance.Sort();
                if (distance.Count != 0)
                {
                    k = distance[0].Item2;
                    _numbersOfStops.Add(distance[0].Item2);
                }
                kol++;
            }
            return _numbersOfStops;
        }

        public void NormalizationStops()
        {
            List<Route> routes = _transportContext.Routes.ToList();
            List<Stop> stops = _transportContext.Stops.ToList();
            foreach(var route in routes)
            {
                List<(double, double)> stops_coordinates = new();
                if (route.StopsId.Count != 0)
                {
                    foreach (var stopId in route.StopsId)
                    {
                        Stop current_stop = stops.FirstOrDefault(s => s.Id == stopId);
                        stops_coordinates.Add(new(current_stop.Latitude, current_stop.Longitude));
                    }
                    List<int> new_stops_order = SearchWay(stops_coordinates);

                    int old_stops = route.StopsId.Count;
                    List<int> new_stopsId = new();
                    foreach (var index in new_stops_order)
                    {
                        new_stopsId.Add(route.StopsId[index]);
                    }
                    new_routes.Add(new Route()
                    {
                        Type = route.Type,
                        City = route.City,
                        CityId = route.CityId,
                        Number = route.Number,
                        StopsId = new_stopsId
                    });
                }
            }
            PostRoutes();
        }
        private void PostRoutes()
        {
            _transportContext.Routes.RemoveRange(_transportContext.Routes.ToList());
            foreach (var route in new_routes)
            {
                _createRoutes.Add(route);
            }
            _transportContext.SaveChanges();
        }
    }
}
