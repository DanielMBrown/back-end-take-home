using GuestlogixBackendTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInformationService.Services
{
    public interface IFlightService
    {
        Task<List<Airline>> GetAirlines();
        Task<List<Airport>> GetAirports();
        Task<List<Route>> GetRoutes();
    }
}
