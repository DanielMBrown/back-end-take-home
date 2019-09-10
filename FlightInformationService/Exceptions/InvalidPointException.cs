using System;

namespace FlightInformationService.Exceptions
{
    public class InvalidPointException : Exception
    {
        public InvalidPointException(string message) : base(message) { }
    }
}
