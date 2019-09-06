using CsvHelper;
using GuestlogixBackendTest.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlightInformationService.Services
{
    public class DataService : IDataService
    {
        public List<Airline> Airlines { get; private set; } = new List<Airline>();
        public List<Airport> Airports { get; private set; } = new List<Airport>();
        public List<Route> Routes { get; private set; } = new List<Route>();

        public void LoadDataFromFile()
        {
            ParseAirlines();
            ParseAirports();
            ParseRoutes();
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
                    // Name,2 Digit Code,3 Digit Code,Country
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
                    // Name,City,Country,IATA 3,Latitute,Longitude
                    var airport = new Airport
                    {
                        Name = csv.GetField("Name"),
                        City = csv.GetField("City"),
                        Country = csv.GetField("Country"),
                        IATADesignator = csv.GetField("IATA 3"),
                        Location = new GeoCoordinatePortable.GeoCoordinate(csv.GetField<double>("Latitute"), csv.GetField<double>("Longitude"))
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
                    // Airline Id,Origin,Destination
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
