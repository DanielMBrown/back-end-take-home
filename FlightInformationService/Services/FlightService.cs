using GuestlogixBackendTest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightInformationService.Services
{
    public class FlightService : IFlightService
    {

        public FlightService()
        {
            // get a copy of a data service that reads in the csv files.
        }

        public Task<List<Airline>> GetAirlines()
        {
            return default;
        }

        public Task<List<Airport>> GetAirports()
        {
            return default;
        }

        public Task<List<Route>> GetRoutes()
        {
            return default;
        }
    }
}
