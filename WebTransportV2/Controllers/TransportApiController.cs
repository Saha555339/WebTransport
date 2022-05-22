using Microsoft.AspNetCore.Mvc;
using LibraryDataBase.Entities;
using LibraryLogic;
using LibraryProjectExceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// <summary>
        /// Getting pairs
        /// </summary>
        /// <remarks>
        /// Getting route pairs with repeated stops
        /// </remarks>
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
            return check?Ok(_logic.PairsOfRoutes.OrderByDescending(s=>s.MatchPercentage)):BadRequest(message);
        }

        /// <summary>
        /// Getting districts
        /// </summary>
        /// <remarks>
        /// Getting districts with repeated stops
        /// </remarks>
        [Route("transport/api/districts")]
        [HttpGet]
        public IActionResult GetDistricts()
        {
            bool check = true;
            string message = "Ok";
            try
            {
                _logic.SearchDistrictsWithRepeatedStops();
            }
            catch (LogicExceptions ex)
            {
                check = false;
                message = ex.Message;
            }
            return check ? Ok(_logic.DistrictStops) : BadRequest(message);
        }

        ///<summary>
        /// All routes
        /// </summary>
        /// <remarks>
        /// Testing all routes
        /// </remarks>
        [Route("transport/api/allroutes")]
        [HttpGet]
        public IActionResult AllRoutes()
        {
            bool check = true;
            string message = "Ok";
            try
            {
                _logic.CollectAllRoutes();
            }
            catch (LogicExceptions ex)
            {
                check = false;
                message = ex.Message;
            }
            return check ? Ok(_logic.AllRoutes) : BadRequest(message);
        }

    }
}
