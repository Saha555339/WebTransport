using System.Collections.Generic;
using LibraryDto;
using LibraryDataBase.Entities;
using System.Linq;
using LibraryProjectExceptions;

namespace LibraryLogic
{
    public class Logic : ILogic
    {
        private static TransportContext _dbContext;

        private List<City> _dbCiteis = new();
        private List<District> _dbDistricts = new();
        private List<Route> _dbRoutes = new();
        private List<Stop> _dbStops = new();
        public Logic(TransportContext dbContext)
        {
            _dbContext = dbContext;
            _dbCiteis = _dbContext.Cities.ToList();
            _dbDistricts = _dbContext.Districts.ToList();
            _dbRoutes = _dbContext.Routes.ToList();
            _dbStops = _dbContext.Stops.ToList();
        }


        private List<StopDto> _stops = new();
        public List<StopDto> Stops
        {
            get { return _stops; }
        }
        private List<PairRoutes> _pairsOfRoutes = new();
        public List<PairRoutes> PairsOfRoutes
        {
            get { return _pairsOfRoutes; }
        }
        
        public void SearchPairsOfRoutes()
        {
            for (int i = 0; i < _dbRoutes.Count; i++)
            {
                for (int j = i+1; j < _dbRoutes.Count; j++)
                {
                    var repeated_stops = _dbRoutes[i].StopsId.Intersect(_dbRoutes[j].StopsId).ToList();
                    var kol1 = _dbRoutes[i].StopsId.Count;
                    var kol2 = _dbRoutes[j].StopsId.Count;
                    const double k = 0.6;
                    if ( ((double)repeated_stops.Count * 2) / (kol1 + kol2) > k)
                    {
                        List<string> route_names = new();
                        route_names.Add(_dbRoutes[i].Number);
                        route_names.Add(_dbRoutes[j].Number);
                        var first_stops = ParseStops(repeated_stops, route_names);
                        var second_stops = ParseStops(repeated_stops, route_names);
                        if (first_stops != null && second_stops != null)
                        {
                            _pairsOfRoutes.Add(new PairRoutes()
                            {
                                FirstRoute = new()
                                {
                                    Id = _dbRoutes[i].Id,
                                    Number = _dbRoutes[i].Number,
                                    Stops = first_stops,
                                    Type = _dbRoutes[i].Type
                                },
                                SecondRoute = new()
                                {
                                    Id = _dbRoutes[j].Id,
                                    Number = _dbRoutes[j].Number,
                                    Stops = second_stops,
                                    Type = _dbRoutes[j].Type
                                }
                            });
                        }
                    }
                }
            }
            if (_pairsOfRoutes.Count == 0)
                throw new LogicExceptions("Found pairs error");
        }

        private List<StopDto> ParseStops(List<int> stopsId, List<string> route_name)
        {
            List<StopDto> repeatedStops = new();
            foreach(var id in stopsId)
            {
                var stop = _dbStops.FirstOrDefault(s => s.Id == id);
                var district = _dbDistricts.FirstOrDefault(s => s.Id == stop.DistrictId);
                if (stop != null && district != null)
                {
                    repeatedStops.Add(new StopDto()
                    {
                         Id = stop.Id,
                         Name = stop.Name,
                         District = district.Name,
                         DistrictId = district.Id,
                         Latitude = stop.Latitude,
                         Longitude = stop.Longitude
                    });
                }
            }
            return repeatedStops.Count!=0?repeatedStops:null;
        }

        private StopDto ParseOneStop(int stopId, List<string> route_numbers)
        {
            StopDto z = null;
            var stop = _dbStops.FirstOrDefault(s => s.Id == stopId);
            var district = _dbDistricts.FirstOrDefault(s => s.Id == stop.DistrictId);
            if (stop != null && district != null)
            {
                z = new StopDto()
                {
                    Id = stop.Id,
                    Name = stop.Name,
                    DistrictId = district.Id,
                    District = district.Name,
                    Latitude = stop.Latitude,
                    Longitude = stop.Longitude,
                    //Routes = route_numbers
                };
            }
            return z;
        }

        public void SearchRepeatedStops()
        {
            SearchPairsOfRoutes();
            foreach (var stop in _dbStops)
            {
                List<string> numbers = new();
                for (int i = 0; i < _pairsOfRoutes.Count; i++)
                {
                    if (_pairsOfRoutes[i].FirstRoute.Stops.FirstOrDefault(s => s.Name == stop.Name) != null)
                    {
                        numbers.Add(_pairsOfRoutes[i].FirstRoute.Number);
                        numbers.Add(_pairsOfRoutes[i].SecondRoute.Number);
                    }
                }
                if (numbers.Count > 0)
                {
                    numbers = numbers.Distinct().ToList();
                    _stops.Add(ParseOneStop(stop.Id, numbers));
                }
            }
        }
    }
}
