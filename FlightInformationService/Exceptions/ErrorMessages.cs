namespace FlightInformationService.Exceptions
{
    public class ErrorMessages
    {
        public const string OriginNotFound = "Invalid Origin - {0}.";
        public const string DestinationNotFound = "Invalid Destination - {0}.";
        public const string RouteNotFound = "Unable to find a route between {0} and {1}.";
        public const string MissingRequiredParameters = "Unable to determine a route due to one or more missing required parameters.";
        public const string UnkownError = "An unknown error occured";
    }
}
