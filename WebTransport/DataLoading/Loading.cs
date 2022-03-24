using WebTransport.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTransport.DataParse;
using WebTransport.ProjectExceptions;

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

        private StopsFileParser _parseStops = new StopsFileParser(@"C:\lab\WebTransport\OnlyStopsCoordinates.csv");
        private RouteFileParser _parseRoutes = new RouteFileParser(@"C:\lab\WebTransport\OnlyStops&Routes.csv");

        private void StopsLoading()
        {
            for(int i=0;i<_parseRoutes.RouteStops.Count;i++)
            {
                int number = 0;
                var route = _dbContext.Routes.FirstOrDefault(s => s.Number == _parseRoutes.RouteStops[i].Number);
                if (route != null)
                {
                    foreach (var stop in _parseRoutes.RouteStops[i].Stops.Distinct().ToList())
                    {
                        var stop_routes = _parseStops.Stops.FirstOrDefault(s => s.Name.Contains(stop));
                        if (stop_routes != null)
                        {
                            number++;
                            _createStops.Add(new Stop()
                            {
                                Name = stop,
                                StopNumber = number,
                                RouteId = route.Id,
                                Latitude = stop_routes.Latitude,
                                Longitude = stop_routes.Longitude,
                                Route = route
                            });
                        }
                    }
                }
                else
                    throw new TransportDataBaseException("Маршрута с таким номером не существует");
            }
            _dbContext.SaveChanges();

        }

        private void RoutesLoading()
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Name == "Москва");
            if (city != null)
            {
                foreach (var route in _parseRoutes.RouteStops)
                    _createRoutes.Add(new Route() { Type = route.Type, CityId = city.Id, Number = route.Number, City = city });
            }
            else
                throw new TransportDataBaseException("Города с таким названием не существует");
            _dbContext.SaveChanges();
        }

        private void DistrictsLoading()
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Name == "Москва");
            List<string> names = _parseStops.Stops.Select(s => s.DistcrictName).Distinct().ToList();
            if (city != null)
            {
                foreach (var name in names)
                {
                    _createDistricts.Add(new District() { Name = name, CityId = city.Id, City = city });
                }
            }
            else
                throw new TransportDataBaseException("Города с таким названием не существует");
            _dbContext.SaveChanges();
        }

        private void CitiesLoading()
        {
            var spec = _dbContext.Cities
                .Where(s => s.Name == "Москва")
                .ToList();
            if (spec.Count != 0)
                throw new TransportDataBaseException("Город с таким названием уже существует");
            else
                _createCities.Add(new City() { Name = "Москва" });
            _dbContext.SaveChanges();
        }

        public void AllLoading()
        {
            bool check = true;
            try
            {
                _parseRoutes.ParseRoutes();
                _parseStops.ParseStops();
            }
            catch(TransportParseException ex)
            {
                Console.WriteLine(ex.Message);
                check = false;
            }
            if (check)
            {
                try
                {
                    CitiesLoading();
                    DistrictsLoading();
                    RoutesLoading();
                    StopsLoading();
                }
                catch (TransportDataBaseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
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