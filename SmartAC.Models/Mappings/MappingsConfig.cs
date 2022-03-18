using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Mappings
{
    public static class MappingsConfig
    {
        public static List<Profile> GetMappingsProfiles()
        {
            return new List<Profile>
            {
                new SensorReadingMapping(),
                new AlertsMapping(),
                new DevicesMappings()
            };
        }
    }
}
