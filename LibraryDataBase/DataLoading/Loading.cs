using LibraryDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryDataParse;
using LibraryProjectExceptions;
using Microsoft.Extensions.Configuration;

namespace LibraryDataBase.DataLoading
{
    public class Loading
    {
        private TransportContext _dbContext;

        private CreateAction<City> _createCities;
        private CreateAction<District> _createDistricts;
        private CreateAction<Route> _createRoutes;
        private CreateAction<Stop> _createStops;

        private IConfiguration _configuration;

        private StopsFileParser _parseStops;
        private RouteFileParser _parseRoutes;

        public Loading(TransportContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _createCities = new CreateAction<City>(_dbContext);
            _createDistricts = new CreateAction<District>(_dbContext);
            _createRoutes = new CreateAction<Route>(_dbContext);
            _createStops = new CreateAction<Stop>(_dbContext);
            _parseStops = new StopsFileParser(_configuration["pathStops"]);
            _parseRoutes = new RouteFileParser(_configuration["pathRoutes"]);

        }


        private void StopsLoading()
        {
            foreach(var stop in _parseStops.Stops)
            {
                var district = _dbContext.Districts.FirstOrDefault(s => s.Name == stop.DistcrictName);
                if (district != null)
                {
                    _createStops.Add(new Stop()
                    {
                        Name = stop.Name,
                        Longitude = stop.Longitude,
                        Latitude = stop.Latitude,
                        DistrictId = district.Id,
                        District = district
                    });
                }
            }
            _dbContext.SaveChanges();
        }
        private void RoutesLoading()
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Name == "Москва");
            if(city!=null)
            {
                foreach(var route in _parseRoutes.RouteStops)
                {
                    var new_route = new Route() { Number = route.Number, CityId = city.Id, City = city, Type = route.Type };
                    List<int> stopsId = new();
                    foreach (var stop in route.Stops)
                    {
                        var routestop = _dbContext.Stops.FirstOrDefault(s => s.Name.Contains(stop));
                        if(routestop!=null)
                        {
                            stopsId.Add(routestop.Id);
                        }
                    }
                    new_route.StopsId = stopsId;
                    _createRoutes.Add(new_route);
                }
            }
            else
                throw new TransportDataBaseException("The city with that name doesn't exist");
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
            var city = _dbContext.Cities
                .Where(s => s.Name == "Москва")
                .ToList();
            if (city.Count != 0)
                throw new TransportDataBaseException("Город с таким названием уже существует");
            else
                _createCities.Add(new City() { Name = "Москва"});
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
                    StopsLoading();
                    RoutesLoading();
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