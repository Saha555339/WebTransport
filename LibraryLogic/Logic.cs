using System.Collections.Generic;
using LibraryDto;
using LibraryDataBase.Entities;
using System.Linq;
using LibraryProjectExceptions;
using System;

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

        private List<PairRoutes> _pairsOfRoutes = new();
        public List<PairRoutes> PairsOfRoutes
        {
            get { return _pairsOfRoutes; }
        }

        private List<DistrictDto> _districtStops = new();
        public List<DistrictDto> DistrictStops
        {
            get { return _districtStops; }
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
                    double matchPercentage = ((double)repeated_stops.Count * 2) / (kol1 + kol2);
                    if (matchPercentage > k)
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
                                    //Stops = first_stops,
                                    Type = _dbRoutes[i].Type
                                },
                                SecondRoute = new()
                                {
                                    Id = _dbRoutes[j].Id,
                                    Number = _dbRoutes[j].Number,
                                    //Stops = second_stops,
                                    Type = _dbRoutes[j].Type
                                },
                                Stops = first_stops,
                                MatchPercentage = matchPercentage
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

        public void SearchDistrictsWithRepeatedStops()
        {
            SearchPairsOfRoutes();
            foreach(var pair in _pairsOfRoutes)
            {
                List<int> districtsId = new();
                foreach(var stop in pair.Stops)
                {
                    var index_stop = StopDistrictIndex(_districtStops, stop.DistrictId);
                    Tuple<int, int> routePairId = new(pair.FirstRoute.Id, pair.SecondRoute.Id);
                    if (index_stop==-1)
                    {
                        _districtStops.Add(new DistrictDto { Id = stop.DistrictId, Name = stop.District, StopsCount = 1 });
                        _districtStops[_districtStops.Count - 1].RoutePairsId.Add(routePairId);
                        districtsId.Add(stop.DistrictId);
                    }
                    else
                    {
                        _districtStops[index_stop].StopsCount++;
                        _districtStops[index_stop].RoutePairsId.Add(routePairId);
                        districtsId.Add(stop.DistrictId);
                    }
                }
                for(int i=0;i<_districtStops.Count;i++)
                {

                    var districts = districtsId.Distinct();
                    _districtStops[i].RoutePairsId = _districtStops[i].RoutePairsId.Distinct().ToList();
                    foreach (var distr in districts)
                        if (distr == _districtStops[i].Id)
                            _districtStops[i].RoutePairsCount++;
                }
            }
        }

        private static int StopDistrictIndex(List<DistrictDto> districts, int districtId)
        {
            var index = -1;
            for (int i = 0; i < districts.Count; i++)
                if (districts[i].Id == districtId)
                    return i;
            return index != -1 ? index : -1;
        }
    }
}
