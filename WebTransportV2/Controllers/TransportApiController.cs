﻿using Microsoft.AspNetCore.Mvc;
using LibraryDataBase.Entities;
using LibraryLogic;
using LibraryProjectExceptions;
using System.Collections.Generic;

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
            return check?Ok(_logic.PairsOfRoutes):BadRequest(message);
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

        //[Route("transport/api/stops")]
        //[HttpGet]
        //public IActionResult GetStops()
        //{
        //    bool check = true;
        //    string message = "Ok";
        //    try
        //    {
        //        _logic.SearchRepeatedStops();
        //    }
        //    catch (LogicExceptions ex)
        //    {
        //        check = false;
        //        message = ex.Message;
        //    }
        //    return check ? Ok(_logic.Stops) : BadRequest(message);
        //}
    }
}