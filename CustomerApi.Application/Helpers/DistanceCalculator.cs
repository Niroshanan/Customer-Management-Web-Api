using CustomerApi.Application.DTOs;
using CustomerApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Application.Helpers
{
    public class DistanceCalculator
    {
        private const double EarthRadiusKm = 6371.0; // Earth radius in kilometers

        public static double CalculateDistance(Customer customer , CordinateDto cordinateDto)
        {
            // Convert latitude and longitude from degrees to radians
            double lat1 = ToRadians(customer.Latitude);
            double lon1 = ToRadians(customer.Longitude);
            double lat2 = ToRadians(cordinateDto.Latitude);
            double lon2 = ToRadians(cordinateDto.Longitude);

            // Calculate differences in coordinates
            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            // Haversine formula
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Calculate distance
            double distance = EarthRadiusKm * c;

            return distance;
        }

        private static double ToRadians(double degree)
        {
            return degree * (Math.PI / 180.0);
        }
    }
}
