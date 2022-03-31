using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTransport.DataBase;
using System.Linq;

namespace WebTransport.Controllers
{
    [Route("transport/api")]
    [ApiController]
    public class TransportApiController : ControllerBase
    {
        private TransportContext _dbContext;
        public TransportApiController(TransportContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Cities.ToList());
        }
    }
}
