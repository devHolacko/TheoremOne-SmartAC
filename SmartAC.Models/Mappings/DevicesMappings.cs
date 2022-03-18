using AutoMapper;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Mappings
{
    public class DevicesMappings : Profile
    {
        public DevicesMappings()
        {
            CreateMap<DeviceRegisteration, DeviceRegisterationViewModel>()
                .ForMember(x => x.RegisterationId, d => d.MapFrom(u => u.Id))
                .ForMember(x => x.DeviceId, d => d.MapFrom(u => u.DeviceId))
                .ForMember(x => x.FirmwareVersion, d => d.MapFrom(u => u.FirmwareVersion))
                .ForMember(x => x.Serial, d => d.MapFrom(u => u.Device.Serial))
                .ForMember(x => x.RegisteredOn, d => d.MapFrom(c => c.CreatedOn))
                .ReverseMap();
        }
    }
}
