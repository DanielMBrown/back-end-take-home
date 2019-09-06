using GuestlogixBackendTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightInformationService.Services
{
    public interface IFlightService
    {
        Task<List<Airline>> GetAirlines();
        Task<List<Airport>> GetAirports();
        Task<List<Route>> GetRoutes();
        Task<Route> RouteFindShortestPath(string origin, string destination);
    }
}
