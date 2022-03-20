using WebTransport.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTransport.DataParse;

namespace WebTransport.DataLoading
{
    public class Loading
    {
        private TransportContext _dbContext;

        private Create<City> _createCities;
        private Create<District> _createDistricts;
        private Create<Route> _createRoutes;
        private Create<Stop> _createStops;

        public Loading(TransportContext dbContext)
        {
            _dbContext = dbContext;
            _createCities = new Create<City>(_dbContext);
            _createDistricts = new Create<District>(_dbContext);
            _createRoutes = new Create<Route>(_dbContext);
            _createStops = new Create<Stop>(_dbContext);
        }

        private List<City> _cities = new List<City>();
        private List<District> _districts = new List<District>();
        private List<Route> _routes = new List<Route>();
        private List<Stop> _stops = new List<Stop>();

        private StopsFileParser _parseStops = new StopsFileParser(@"C:\lab\WebTransport\OnlyStopsCoordinates.csv");
        private RouteFileParser _parseRoutes = new RouteFileParser(@"C:\lab\WebTransport\OnlyStops&Routes.csv");

        private void StopsLoading()
        {
            //for(int i=0;i<_parseStops.StopNames.Count;i++)
            //{
            //    var district = _dbContext.Districts.FirstOrDefault(s=>s.Name == _parseStops.DistrictNames[i]);
            //    var routes = _parseRoutes.RouteStops.Where(s => s.Stops.FirstOrDefault(s => s.Contains(_parseStops.StopNames[i])) != null).ToList();
            //    foreach(var route in routes)
            //    {
            //        var route_tek = _dbContext.Routes.FirstOrDefault(s => s.Number == route.Number);
            //        _createStops.Add(new Stop()
            //        {
            //            Name = _parseStops.StopNames[i],
            //            Latitude = _parseStops.Latitudes[i],
            //            Longitude = _parseStops.Longitudes[i],
            //            DistrictId = district.Id,
            //            RouteId = route_tek.Id
            //        });
            //    }
            //}

            for (int i = 0; i < _parseRoutes.RouteStops.Count; i++)
            {
                var route = _dbContext.Routes.FirstOrDefault(s => s.Number == _parseRoutes.RouteStops[i].Number);
                foreach (var stop in _parseRoutes.RouteStops[i].Stops)
                {
                    _createStops.Add(new Stop() { Name = stop, RouteId = route.Id });
                }
            }
            _dbContext.SaveChanges();

        }

        private void RoutesLoading()
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Name == "Москва");
            foreach(var route in _parseRoutes.RouteStops)
                _createRoutes.Add(new Route() { Type = route.Type, CityId = city.Id, Number = route.Number });
            _dbContext.SaveChanges();
        }

        private void DistrictsLoading()
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Name == "Москва");
            List<string> names = _parseStops.DistrictNames.Distinct().ToList();
            foreach(var name in names)
            {
                _createDistricts.Add(new District() { Name = name, CityId = city.Id });
            }
            _dbContext.SaveChanges();
        }

        private void CitiesLoading()
        {
            var spec = _dbContext.Cities
                .Where(s => s.Name == "Москва")
                .ToList();
            if (spec.Count != 0)
                throw new Exception("Город с таким названием уже существует");
            else
                _createCities.Add(new City() { Name = "Москва" });
            _dbContext.SaveChanges();
        }

        public void AllLoading()
        {
            _parseRoutes.ParseRoutes();
            _parseStops.ParseStops();
            CitiesLoading();
            DistrictsLoading();
            RoutesLoading();
            StopsLoading();
        }
        public void RemoveAll()
        {
            _dbContext.Stops.RemoveRange(_dbContext.Stops.ToList());
            _dbContext.Routes.RemoveRange(_dbContext.Routes.ToList());
            _dbContext.Districts.RemoveRange(_dbContext.Districts.ToList());
            _dbContext.Cities.RemoveRange(_dbContext.Cities.ToList());
            _dbContext.SaveChanges();

        }
    }
}
