using LibraryProjectExceptions;
using System.Linq;
using LibraryDataBase.Entities;
using System.Collections.Generic;

namespace LibraryDataBase.DataLoading
{
    public class PostEntites
    {
        private TransportContext _dbContext;
        private Create<City> _createCity;
        private Create<District> _createDistrict;
        private Create<Route> _createRoute;
        private Create<Stop> _createStop;

        public PostEntites(TransportContext dbContext)
        {
            _dbContext = dbContext;
            _createCity = new(_dbContext);
            _createDistrict = new(_dbContext);
            _createRoute = new(_dbContext);
            _createStop = new(_dbContext);
        }

        public void PostCity(string name)
        {
            var city = _dbContext.Cities
                .Where(s => s.Name == name)
                .ToList();
            if (city.Count != 0)
                throw new TransportDataBaseException("The city with that name already exists");
            else
                _createCity.Add(new City() { Name = name });
            _dbContext.SaveChanges();
        }

        public void PostDistrict(string name, int cityId)
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Id == cityId);
            if (city != null)
                _createDistrict.Add(new District() { Name = name, CityId = cityId, City = city });
            else
                throw new TransportDataBaseException("The city with that Id doesn't exists");
        }
        public void PostRoute(string number, string type, int cityId, List<int> stopsId)
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Id == cityId);
            if (city != null)
                _createRoute.Add(new Route() { Number = number, Type = type, CityId = cityId, City = city, StopsId = stopsId});
            else
                throw new TransportDataBaseException("The city with that Id doesn't exists");
        }
        public void PostStop(string name, double latitude, double longitude, int districtId)
        {
            var district = _dbContext.Districts.FirstOrDefault(s => s.Id == districtId);
            if (district != null)
                _createStop.Add(new Stop() { Name = name, Longitude = longitude, Latitude = latitude, DistrictId = districtId, District = district });
            else
                throw new TransportDataBaseException("The route with that Id doesn't exists");
        }

    }
}
