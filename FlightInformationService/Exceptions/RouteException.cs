using System;

namespace FlightInformationService.Exceptions
{
    public class RouteException : Exception
    {
        public RouteException(string message) : base(message) { }
    }
}
