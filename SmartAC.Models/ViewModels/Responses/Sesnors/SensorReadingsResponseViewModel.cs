using SmartAC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Responses.Sesnors
{
    public class SensorReadingsResponseViewModel
    {
        public Guid SensorReadingId { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal CarbonMonoxide { get; set; }
        public string HealthStatus { get; set; }
        public Guid AlertId { get; set; }
    }
}
