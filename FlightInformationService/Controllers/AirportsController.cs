using System.Collections.Generic;
using System.Threading.Tasks;
using FlightInformationService.Services;
using GuestlogixBackendTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightInformationService.Controllers
{
    [Route("api/v1/airports")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IFlightService flightService;

        public AirportsController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Airport>>> GetAirports()
        {
            var routes = await flightService.GetAirports();

            return routes;
        }
    }
}