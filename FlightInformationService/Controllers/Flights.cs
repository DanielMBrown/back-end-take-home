using System.Collections.Generic;
using System.Threading.Tasks;
using FlightInformationService.Services;
using GuestlogixBackendTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightInformationService.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class Flights : ControllerBase
    {
        private readonly IFlightService flightService;

        public Flights (IFlightService flightService)
        {
            this.flightService = flightService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<Route>>> Get()
        {
            var routes = await flightService.GetRoutes();

            return routes;
        }
    }
}
