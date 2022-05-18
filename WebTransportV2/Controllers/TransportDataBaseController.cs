using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LibraryDataBase.Entities;
using System.Linq;
using LibraryDataBase.DataLoading;
using LibraryProjectExceptions;
using Microsoft.Extensions.Configuration;
using DataCleaning;

namespace WebTransport.Controllers
{
    [ApiController]
    public class TransportDataBaseController: ControllerBase
    {
        private TransportContext _dbContext;
        private Loading _loading;
        private PostEntites _postEntites;
        private Cleaning _cleaning;
        private NormalizationAlgorithm _normalAlg;
        public TransportDataBaseController(TransportContext dbContext, IConfiguration configuraton)
        {
            _dbContext = dbContext;
            _loading = new(_dbContext, configuraton);
            _postEntites = new(_dbContext);
            _cleaning = new(_dbContext);
            _normalAlg = new(_dbContext);
        }

        #region Get

        [HttpGet]
        [Route("database/cities/{id?}")]
        public IActionResult GetCity(int? id)
        {
            var city = _dbContext.Cities.FirstOrDefault(s => s.Id == id);
            return city != null ? Ok(city):NotFound();
        }
        [HttpGet]
        [Route("database/districts/{id?}")]
        public IActionResult GetDistrict(int? id)
        {
            var district = _dbContext.Districts.FirstOrDefault(s => s.Id == id);
            return district!=null?Ok(district):NotFound();
        }
        [HttpGet]
        [Route("database/routes/{id?}")]
        public IActionResult GetRoutes(int? id)
        {
            var route = _dbContext.Routes.FirstOrDefault(s => s.Id == id);
            return route!=null?Ok(route):NotFound();
        }
        [HttpGet]
        [Route("database/stops/{id?}")]
        public IActionResult GetStops(int? id)
        {
            return Ok(_dbContext.Stops.FirstOrDefault(s => s.Id == id));
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("database/delete/all")]
        public string DeleteAll()
        {
            string answer = "All data deleted";
            try
            {
                _loading.RemoveAll();
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            
            return answer;
        }
        [HttpDelete]
        [Route("database/delete/city/{id}")]
        public string DeleteCity(int id)
        {
            string answer = $"City with id {id} deleted";
            DeleteAction<City> delete = new(_dbContext);
            try
            {
                delete.DeleteEntity(id);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }
        [HttpDelete]
        [Route("database/delete/district/{id}")]
        public string DeleteDistrict(int id)
        {
            string answer = $"City with id {id} deleted";
            DeleteAction<District> delete = new(_dbContext);
            try
            {
                delete.DeleteEntity(id);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }
        [HttpDelete]
        [Route("database/delete/route/{id}")]
        public string DeleteRoute(int id)
        {
            string answer = $"City with id {id} deleted";
            DeleteAction<Route> delete = new(_dbContext);
            try
            {
                delete.DeleteEntity(id);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }
        [HttpDelete]
        [Route("database/delete/stop/{id}")]
        public string DeleteStop(int id)
        {
            string answer = $"City with id {id} deleted";
            DeleteAction<Stop> delete = new(_dbContext);
            try
            {
                delete.DeleteEntity(id);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }
        #endregion

        #region Post
        /// <summary>
        /// Data cleaning in route stops
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("database/cleaning")]
        public string PostCleanRoutes()
        {
            string answer = "All data clean";
            try
            {
                _cleaning.CleanRouteStops();
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }

        /// <summary>
        /// Data normalization in route stops
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("database/normalize")]
        public string PostNormalizationRoutes()
        {
            string answer = "All data normalize";
            try
            {
                _normalAlg.NormalizationStops();
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }

        [HttpPost]
        [Route("database/post/all")]
        public string PostAllData()
        {
            string answer = "All data loaded";
            try
            {
                _loading.AllLoading();
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }
        [HttpPost]
        [Route("database/post/city")]
        public IActionResult PostCity(string name)
        {
            string answer = "City loaded";
            try
            {
                _postEntites.PostCity(name);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer== "City loaded"?Ok(_dbContext.Cities.First(s=>s.Name==name)):BadRequest(answer);
        }
        [HttpPost]
        [Route("database/post/district")]
        public IActionResult PostDistrict(string name, int cityId)
        {
            string answer = "District loaded";
            try
            {
                _postEntites.PostDistrict(name, cityId);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer == "District loaded" ? Ok(_dbContext.Districts.First(s => s.Name == name)) : BadRequest(answer);
        }
        [HttpPost]
        [Route("database/post/stop")]
        public IActionResult PostStop(string name, double latitude, double longitude, int districtId)
        {
            string answer = "Stop loaded";
            try
            {
                _postEntites.PostStop(name, latitude, longitude, districtId);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer == "Stop loaded" ? Ok(_dbContext.Stops.First(s => s.Name == name)) : BadRequest(answer);
        }
        [HttpPost]
        [Route("database/post/route")]
        public IActionResult PostRoute(string number, string type, int cityId, List<int> stopsId)
        {
            string answer = "Route loaded";
            try
            {
                _postEntites.PostRoute(number, type, cityId, stopsId);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer == "Route loaded" ? Ok(_dbContext.Routes.First(s => s.Number == number)) : BadRequest(answer);
        }
        #endregion

    }
}
