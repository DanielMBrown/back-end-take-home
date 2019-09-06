using GuestlogixBackendTest.Models;
using System.Collections.Generic;

namespace FlightInformationService.Services
{
    public interface IDataService
    {
        List<Route> Routes { get; }
        List<Airline> Airlines { get; }
        List<Airport> Airports { get; }
    }
}
