using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerApi.Application.DTOs
{
    public class CordinateDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
