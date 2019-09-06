using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FlightInformationService.Services;
using GuestlogixBackendTest.Models;
using Microsoft.AspNetCore.Mvc;


namespace FlightInformationService.Controllers
{
    [Route("api/v1/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {

        private readonly IFlightService flightService;

        public RoutesController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Route>))]
        public async Task<ActionResult<List<Route>>> Routes()
        {
            return Ok(await flightService.GetRoutes());
        }

        [HttpGet("shortestPath",Name = nameof(IFlightService.RouteFindShortestPath))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Route))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResult))]
        public async Task<ActionResult<Route>> RouteFindShortestPath(string origin, string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return BadRequest(new { message = "Unable to determine a route due to one or more missing required parameters." });
            }

            return Ok(await flightService.RouteFindShortestPath(origin, destination));
        }
    }
}