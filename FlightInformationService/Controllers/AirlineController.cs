using System.Collections.Generic;
using System.Threading.Tasks;
using FlightInformationService.Services;
using GuestlogixBackendTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightInformationService.Controllers
{
    [Route("api/airlines")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IFlightService flightService;

        public AirlineController (IFlightService flightService)
        {
            this.flightService = flightService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<Airline>>> GetAirlines()
        {
            var routes = await flightService.GetAirlines();

            return routes;
        }
    }
}
