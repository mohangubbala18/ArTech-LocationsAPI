namespace LocationsAPI.Models
{
    public class Location
    {
        public string? LocationName { get; set; }
        public List<Availability>? Availabilities { get; set; }
    }
}
