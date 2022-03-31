using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebTransport.DataBase;
using System.Linq;
using WebTransport.DataLoading;
using WebTransport.ProjectExceptions;

namespace WebTransport.Controllers
{
    [ApiController]
    public class TransportDataBaseController: ControllerBase
    {
        private TransportContext _dbContext;
        private Loading _loading;
        private PostEntites _postEntites;
        public TransportDataBaseController(TransportContext dbContext)
        {
            _dbContext = dbContext;
            _loading = new(_dbContext);
            _postEntites = new(_dbContext);
        }

        #region Get

        [HttpGet]
        [Route("database/cities/{id?}")]
        public IActionResult GetCity(int? id)
        {
            return Ok(_dbContext.Cities.FirstOrDefault(s=>s.Id==id));
        }
        [HttpGet]
        [Route("database/districts/{id?}")]
        public IActionResult GetDistrict(int? id)
        {
            return Ok(_dbContext.Districts.FirstOrDefault(s=>s.Id==id));
        }
        [HttpGet]
        [Route("database/routes/{id?}")]
        public IActionResult GetRoutes(int? id)
        {
            return Ok(_dbContext.Routes.FirstOrDefault(s=>s.Id==id));
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
            Delete<City> delete = new(_dbContext);
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
            Delete<District> delete = new(_dbContext);
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
            Delete<Route> delete = new(_dbContext);
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
            Delete<Stop> delete = new(_dbContext);
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
        public string PostCity(string name)
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
            return answer;
        }
        [HttpPost]
        [Route("database/post/district")]
        public string PostDistrict(string name, int cityId)
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
            return answer;
        }
        [HttpPost]
        [Route("database/post/stop")]
        public string PostStop(string name, double latitude, double longitude, int districtId)
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
            return answer;
        }
        [HttpPost]
        [Route("database/post/route")]
        public string PostRoute(string name, string type, int cityId, List<int> stopsId)
        {
            string answer = "Route loaded";
            try
            {
                _postEntites.PostRoute(name, type, cityId, stopsId);
            }
            catch (TransportDataBaseException ex)
            {
                answer = ex.Message;
            }
            return answer;
        }
        #endregion

    }
}
