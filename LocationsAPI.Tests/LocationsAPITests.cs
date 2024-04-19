using System;
using System.Collections.Generic;
using System.Linq;
using LocationsAPI.Controllers;
using LocationsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace LocationsAPI.Tests
{
    public class LocationsControllerTests
    {
        [Fact]
        public void GetLocations_ReturnsLocationsWithAvailabilityBetween10And1()
        {
            // Arrange
            var controller = new LocationsController();

            // Act
            var actionResult = controller.GetLocations();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var locations = Assert.IsAssignableFrom<IEnumerable<Location>>(okObjectResult.Value);
            Assert.NotNull(locations);

            foreach (var location in locations)
            {
                Assert.NotNull(location);

                // Check if Availabilities is not null
                if (location.Availabilities != null)
                {
                    foreach (var availability in location.Availabilities)
                    {
                        if (availability.OpenTime != null && availability.CloseTime != null)
                        {
                            var openTime = TimeSpan.Parse(availability.OpenTime);
                            var closeTime = TimeSpan.Parse(availability.CloseTime);
                            Assert.True(openTime <= TimeSpan.Parse("13:00") && closeTime >= TimeSpan.Parse("10:00"));
                        }
                    }
                }
            }
        }
    }
}