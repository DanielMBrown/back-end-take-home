using System.Collections.Generic;
using System.Threading.Tasks;
using FlightInformationService.Services;
using GuestlogixBackendTest.Models;
using Microsoft.AspNetCore.Mvc;


namespace FlightInformationService.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RouteController : ControllerBase
    {

        private readonly IFlightService flightService;

        public RouteController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Route>>> GetRoutes()
        {
            var routes = await flightService.GetRoutes();

            return routes;
        }
    }
}