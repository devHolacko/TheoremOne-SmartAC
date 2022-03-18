using AutoMapper;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.ViewModels.Requests.Alerts;
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
        }
    }
}
