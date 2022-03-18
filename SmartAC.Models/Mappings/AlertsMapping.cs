using AutoMapper;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Responses.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Mappings
{
    public class AlertsMapping : Profile
    {
        public AlertsMapping()
        {
            CreateMap(typeof(CreateAlertRequest), typeof(Alert)).ReverseMap();
            CreateMap<Alert, AlertViewModel>()
                .ForMember(x => x.DeviceSerial, d => d.MapFrom(c => c.Device.Serial))
                .ReverseMap();
        }
    }
}
