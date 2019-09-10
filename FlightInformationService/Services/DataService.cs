using CsvHelper;
using GuestlogixBackendTest.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlightInformationService.Services
{
    public class DataService : IDataService
    {
        public List<Airline> Airlines { get; } = new List<Airline>();
        public List<Airport> Airports { get; } = new List<Airport>();
        public List<Route> Routes { get;} = new List<Route>();

        public void LoadDataFromFile()
        {
            ParseAirlines();
            ParseAirports();
            ParseRoutes();
            AssociateRoutesWithAirports();
        }

        private void AssociateRoutesWithAirports()
        {
            foreach(var airport in Airports)
            {
                var routes = Routes.FindAll(x => x.Origin.IATADesignator == airport.IATADesignator);
                airport.Routes = routes;
            }
        }

        private void ParseAirlines()
        {
            Airlines.Clear();

            using (var reader = new StreamReader("data/full/airlines.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var airline = new Airline
                    {
                        Name = csv.GetField("Name"),
                        IATADesignator = csv.GetField("2 Digit Code"),
                        ICAODesignator = csv.GetField("3 Digit Code"),
                        Country = csv.GetField("Country")
                    };

                    Airlines.Add(airline);
                }
            }
        }

        private void ParseAirports()
        {
            Airports.Clear();

            using (var reader = new StreamReader("data/full/airports.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var airport = new Airport
                    {
                        Name = csv.GetField("Name"),
                        City = csv.GetField("City"),
                        Country = csv.GetField("Country"),
                        IATADesignator = csv.GetField("IATA 3"),
                        Latitude = csv.GetField<double>("Latitute"),
                        Longitude = csv.GetField<double>("Longitude")
                    };

                    Airports.Add(airport);
                }
            }
        }

        private void ParseRoutes()
        {
            Routes.Clear();

            using (var reader = new StreamReader("data/full/routes.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var route = new Route
                    {
                        Airline = Airlines.FirstOrDefault(x => x.IATADesignator == csv.GetField("Airline Id")),
                        Origin = Airports.FirstOrDefault(x => x.IATADesignator == csv.GetField("Origin")),
                        Destination = Airports.FirstOrDefault(x => x.IATADesignator == csv.GetField("Destination"))
                    };

                    Routes.Add(route);
                }
            }
        }
    }
}
