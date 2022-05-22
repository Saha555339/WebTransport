using System;
using LibraryDataBase.Entities;
using LibraryDataBase.DataLoading;
using System.Collections.Generic;
using System.Linq;

namespace DataCleaning
{
    public class Cleaning
    {
        private TransportContext _transportContext;
        CreateAction<Route> _createRoutes;
        public Cleaning(TransportContext transportContext)
        {
            _transportContext = transportContext;
            _createRoutes = new CreateAction<Route>(_transportContext);
        }

        private List<Route> _routes;
        private List<Route> _postRoutes = new();
        public void CleanRouteStops()
        {
            _routes = _transportContext.Routes.ToList();
            List<Stop> stops = _transportContext.Stops.ToList();
            foreach(var route in _routes)
            {
                if (route.StopsId.Count > 2)
                {

                    Stop first_stop = stops.FirstOrDefault(s => s.Id == route.StopsId[0]);
                    Stop second_stop = stops.FirstOrDefault(s => s.Id == route.StopsId[1]);

                    if ((first_stop!=null && second_stop!= null) && (first_stop.DistrictId != second_stop.DistrictId))
                        route.StopsId.RemoveAt(0);

                    Stop last_stop = stops.FirstOrDefault(s => s.Id == route.StopsId[route.StopsId.Count - 1]);
                    Stop pre_last = stops.FirstOrDefault(s => s.Id == route.StopsId[route.StopsId.Count - 2]);

                    if ((last_stop!=null && pre_last!=null) && (last_stop.DistrictId != pre_last.DistrictId))
                        route.StopsId.RemoveAt(route.StopsId.Count - 1);
                    for (int i = 1; i < route.StopsId.Count - 1; i++)
                    {
                        Stop current_stop = stops.FirstOrDefault(s => s.Id == route.StopsId[i]);
                        Stop next_stop = stops.FirstOrDefault(s => s.Id == route.StopsId[i + 1]);
                        Stop previous_stop = stops.FirstOrDefault(s => s.Id == route.StopsId[i - 1]);

                        if ((next_stop!=null && current_stop != null && previous_stop!=null) && (current_stop.DistrictId != previous_stop.DistrictId && current_stop.DistrictId != next_stop.DistrictId))
                            route.StopsId.RemoveAt(i);
                    }
                    _postRoutes.Add(new Route() { City = route.City, CityId = route.CityId, Number = route.Number, StopsId = route.StopsId, Type = route.Type });
                }
            }
            PostRoutes();
        }

        private void PostRoutes()
        {
            _transportContext.Routes.RemoveRange(_transportContext.Routes.ToList());
            foreach (var route in _postRoutes)
            {
                _createRoutes.Add(route);
            }
            _transportContext.SaveChanges();
        }
    }
}
