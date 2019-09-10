using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlightInformationService.Exceptions;
using FlightInformationService.Services;
using GuestlogixBackendTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightInformationService.Controllers
{
    [Route("api/v1/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IFlightService flightService;
        private readonly ILogger logger;

        public RoutesController(IFlightService flightService, ILogger<RoutesController> logger)
        {
            this.flightService = flightService;
            this.logger = logger;
        }

        [HttpGet("shortestPath", Name = nameof(IFlightService.RouteFindShortestPath))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(StatusCodeResult))]
        public async Task<ActionResult<List<string>>> RouteFindShortestPath(string origin, string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                logger.LogError(ErrorMessages.MissingRequiredParameters);
                return BadRequest(new { message = ErrorMessages.MissingRequiredParameters });
            }

            try
            {
                return Ok(await flightService.RouteFindShortestPath(origin, destination));
            }
            catch (InvalidPointException ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(new { message = ex.Message, details = ex.StackTrace });
            }
            catch (RouteException ex)
            {
                logger.LogError(ex.Message, ex);
                return NotFound(new { message = ex.Message, details = ex.StackTrace });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ErrorMessages.UnkownError + ex.Message, details = ex.StackTrace });
            }
        }
    }
}