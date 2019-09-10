using GuestlogixBackendTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightInformationService.Services
{
    public interface IFlightService
    {
        Task<List<Airline>> Airlines();
        Task<List<Airport>> Airports();
        Task<List<Route>> Routes();
        Task<List<string>> RouteFindShortestPath(string origin, string destination);
    }
}
