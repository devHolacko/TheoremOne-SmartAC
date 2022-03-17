﻿using SmartAC.Models.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Sensors
{
    public class SensorsReading : BaseAuditedEntity
    {
        public Guid DeviceId { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal CarbonMonoxide { get; set; }
        public string HealthStatus { get; set; }
    }
}
