using AutoMapper;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Mappings
{
    public class SensorReadingMapping : Profile
    {
        public SensorReadingMapping()
        {
            CreateMap(typeof(SensorsReading), typeof(SensorReadingViewModel)).ReverseMap();
            CreateMap<SensorsReading, SensorReadingsResponseViewModel>()
                .ForMember(c => c.HealthStatus, d => d.MapFrom(x => x.HealthStatus.ToString()))
                .ForMember(c => c.SensorReadingId, d => d.MapFrom(x => x.Id))
                .ForMember(c => c.AlertId, d => d.MapFrom(x => x.Alerts != null ? x.Alerts.FirstOrDefault().Id : Guid.Empty))
                .ReverseMap();
        }
    }
}
