using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Services
{
    public interface IDeviceService
    {
        GenericResponse Register(RegisterDeviceRequest request);
        GenericResponse ReportDeviceReadings(ReportDeviceReadingsRequest request);
        DataGenericResponse<List<DeviceRegisterationViewModel>> GetRecentlyRegisteredDevices(int pageNumber = 1, int pageSize = 10);
        DataGenericResponse<List<DeviceViewModel>> FilterDevicesBySerial(string serialNumber);
        DataGenericResponse<DeviceViewModel> GetDevicesByRegisterationDate(DateTime from, DateTime to);
    }
}
