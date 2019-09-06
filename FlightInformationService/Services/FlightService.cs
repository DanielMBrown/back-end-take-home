using GuestlogixBackendTest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightInformationService.Services
{
    public class FlightService : IFlightService
    {
        private readonly DataService dataService;

        public FlightService(DataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<List<Airline>> GetAirlines()
        {
            return dataService.Airlines;
        }

        public async Task<List<Airport>> GetAirports()
        {
            return dataService.Airports;
        }

        public async Task<List<Route>> GetRoutes()
        {
            return dataService.Routes;
        }
    }
}
