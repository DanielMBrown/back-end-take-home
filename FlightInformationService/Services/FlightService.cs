using FlightInformationService.Exceptions;
using FlightInformationService.Models;
using GuestlogixBackendTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInformationService.Services
{
    public class FlightService : IFlightService
    {
        private readonly IDataService dataService;

        public FlightService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<List<Airline>> Airlines()
        {
            return dataService.Airlines;
        }

        public async Task<List<Airport>> Airports()
        {
            return dataService.Airports;
        }

        public async Task<List<Route>> Routes()
        {
            return dataService.Routes;
        }

        public async Task<List<string>> RouteFindShortestPath(string origin, string destination)
        {
            var startingAirport = dataService.Airports.FirstOrDefault(x => x.IATADesignator == origin);
            var endingAirport = dataService.Airports.FirstOrDefault(x => x.IATADesignator == destination);

            if (startingAirport is null)
            {
                throw new InvalidPointException(string.Format(ErrorMessages.OriginNotFound, origin));
            }

            if (endingAirport is null)
            {
                throw new InvalidPointException(string.Format(ErrorMessages.DestinationNotFound, destination));
            }

            var graph = GenerateAirportGraph();
            var visited = new Dictionary<Airport, Airport>();
            var queue = new Queue<Airport>();
            var routeDiscovered = false;

            queue.Enqueue(startingAirport);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == endingAirport)
                {
                    routeDiscovered = true;
                    break;
                }

                foreach (var connectedAirport in graph.AdjacencyList[current])
                {
                    if (!visited.ContainsKey(connectedAirport))
                    {
                        visited[connectedAirport] = current;
                        queue.Enqueue(connectedAirport);
                    }
                }
            }

            var finalPath = new List<Airport>();

            if (routeDiscovered)
            {
                finalPath = GeneratePath(startingAirport, endingAirport, visited);
            }
            else
            {
                throw new RouteException(string.Format(ErrorMessages.RouteNotFound, origin, destination));
            }

            return finalPath.Select(x => x.IATADesignator).ToList();
        }

        private List<Airport> GeneratePath(Airport start, Airport end, Dictionary<Airport, Airport> visited)
        {
            var path = new List<Airport>();
            var current = end;

            // backtrack through the visited graph vertices starting from the destination, working back to the origin.
            while (current != start)
            {
                path.Add(current);
                current = visited[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

        private Graph<Airport> GenerateAirportGraph()
        {
            // Trim the vertices down, slight optimization
            var vertices = dataService.Airports.FindAll(x => x.Routes.Count > 0);
            var edges = new List<Tuple<Airport, Airport>>();

            foreach (var vertex in vertices)
            {
                vertex.Routes.ForEach(x => edges.Add(Tuple.Create(vertex, x.Destination)));
            }

            return new Graph<Airport>(vertices, edges);
        }
    }
}