using LocationsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace LocationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly List<Location> _locations;

        public LocationsController()
        {
            // Initialize locations with sample data
            _locations = new List<Location>
            {
                new Location
                {
                    LocationName = "Library",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "17:00", CloseTime = "20:00" },
                    }
                },
                new Location
                {
                    LocationName = "Coffee Shop",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "06:00", CloseTime = "09:30" },
                    }
                },
                new Location
                {
                    LocationName = "Pharmacy",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "08:00", CloseTime = "20:00" },
                    }
                },
                new Location
                {
                    LocationName = "Bakery",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "07:00", CloseTime = "19:00" },
                    }
                },
                new Location
                {
                    LocationName = "Barber Shop",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "09:00", CloseTime = "18:00" },
                    }
                },
                new Location
                {
                    LocationName = "Supermarket",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "09:00", CloseTime = "22:00" },
                    }
                },
                new Location
                {
                    LocationName = "Candy Store",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "10:00", CloseTime = "18:00" },
                    }
                },
                new Location
                {
                    LocationName = "Cinema Complex",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "11:00", CloseTime = "23:00" },
                    }
                },
                new Location
                {
                    LocationName = "Gym",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "06:00", CloseTime = "22:00" },
                    }
                },
                new Location
                {
                    LocationName = "Restaurant",
                    Availabilities = new List<Availability>
                    {
                        new Availability { OpenTime = "11:00", CloseTime = "23:00" },
                    }
                },
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetLocations()
        {
            // Filter locations with availability overlapping between 10 am and 1 pm
            var locationsWithAvailability = _locations.Where(location =>
                location.Availabilities != null && // Check if Availabilities is not null
                location.Availabilities.Any(availability =>
                {
                    // Parse OpenTime and CloseTime
                    //TimeSpan.TryParse to parse the time strings into TimeSpan objects safely, handling null values
                    if (TimeSpan.TryParse(availability.OpenTime, out TimeSpan openTime) &&
                        TimeSpan.TryParse(availability.CloseTime, out TimeSpan closeTime))
                    {
                        // Check if either OpenTime or CloseTime overlaps with the time range between 10 am and 1 pm
                        return (openTime <= TimeSpan.Parse("13:00") && closeTime >= TimeSpan.Parse("10:00"));
                    }
                    return false; // Return false if parsing fails
                }));

            return Ok(locationsWithAvailability);
        }
    }
}
