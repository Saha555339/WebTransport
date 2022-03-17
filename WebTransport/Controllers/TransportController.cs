using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebTransport.Dto;
using WebTransport.DataBase;
using System.Linq;

namespace WebTransport.Controllers
{
    public class TransportController: ControllerBase
    {
        private TransportContext _dbContext;
        public static List<int> number=new List<int>() { 1, 2, 3,4,5,6, 6,6 };
        List<int> chetn = number.Where(s=>s% 2 != 0).Distinct().ToList();
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
            return Ok(chetn);
        }
    }
}
