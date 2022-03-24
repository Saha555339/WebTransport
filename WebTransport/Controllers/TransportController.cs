using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebTransport.Dto;
using WebTransport.DataBase;
using System.Linq;
using WebTransport.DataLoading;

namespace WebTransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportController: ControllerBase
    {
        private TransportContext _dbContext;
        public TransportController(TransportContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Loading loading = new(_dbContext);
            //_dbContext.Database.EnsureDeleted();
            //loading.AllLoading();
            //loading.RemoveAll();
            return Ok(_dbContext.Stops.Where(s=>s.Route.Number.Contains("М97")).ToList().OrderBy(s=>s.StopNumber));
            //return Ok();
        }
    }
}
