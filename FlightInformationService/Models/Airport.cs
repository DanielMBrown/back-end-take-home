using System.Collections.Generic;

namespace GuestlogixBackendTest.Models
{
    public class Airport
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATADesignator { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<Route> Routes { get; set; }
    }
}
