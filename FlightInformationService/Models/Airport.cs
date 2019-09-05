using GeoCoordinatePortable;



namespace GuestlogixBackendTest.Models
{
    public class Airport
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATADesignator { get; set; }
        public GeoCoordinate Location { get; set; }
}
}
