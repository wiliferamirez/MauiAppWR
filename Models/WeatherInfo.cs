using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppWR.Models
{
    internal class WeatherInfo
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double Rain { get; set; }
    }
}
