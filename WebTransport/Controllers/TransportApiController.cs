using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTransport.DataBase;
using System.Linq;
using WebTransport.EngineLogic;
using WebTransport.ProjectExceptions;

namespace WebTransport.Controllers
{
    [ApiController]
    public class TransportApiController : ControllerBase
    {
        private TransportContext _dbContext;
        private Logic _logic;
        public TransportApiController(TransportContext dbContext)
        {
            _dbContext = dbContext;
            _logic = new(_dbContext);
        }
        [Route("transport/api/routepairs")]
        [HttpGet]
        public IActionResult GetPairs()
        {
            bool check = true;
            string message = "Ok";
            try
            {
                _logic.SearchPairsOfRoutes();
            }
            catch (LogicExceptions ex)
            {
                check = false;
                message = ex.Message;
            }
            //return check?Ok(_logic.PairsOfRoutes):BadRequest(message);
            return Ok(_logic.PairsOfRoutes);
        }

        [Route("transport/api/stops")]
        [HttpGet]
        public IActionResult GetStops()
        {
            bool check = true;
            string message = "Ok";
            try
            {
                _logic.SearchRepeatedStops();
            }
            catch (LogicExceptions ex)
            {
                check = false;
                message = ex.Message;
            }
            return check ? Ok(_logic.Stops) : BadRequest(message);
        }
    }
}
