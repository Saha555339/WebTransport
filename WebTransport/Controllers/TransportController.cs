using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebTransport.Dto;
using WebTransport.DataBase;

namespace WebTransport.Controllers
{
    public class TransportController: ControllerBase
    {
        private readonly TransportContext _dbContext;
        public TransportController(TransportContext dbContext)
        {
            _dbContext = dbContext;
        }
        //private static readonly List<string> _transports = new List<string>()
        //{
        //    new string("Tran1"),
        //    new string("Tran2")
        //};
        //private readonly List<StopDto> stops = new List<StopDto>()
        //{
        //    new StopDto(name:"name1", latitude:14, longitude:12, routes:_transports)
        //};
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
