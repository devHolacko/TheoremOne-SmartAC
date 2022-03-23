using AutoMapper;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses;
using SmartAC.Models.ViewModels.Responses.Devices;
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

            CreateMap<DeviceViewModel, Device>()
                .ForMember(x => x.Id, d => d.MapFrom(c => c.DeviceId))
                .ForMember(x => x.Serial, d => d.MapFrom(c => c.SerialNumber))
                .ReverseMap();

            CreateMap<DeviceRegisteration, RegisterDeviceRequest>().ForMember(c => c.FirmwareVersion, d => d.MapFrom(u => u.FirmwareVersion)).ReverseMap();
        }
    }
}
