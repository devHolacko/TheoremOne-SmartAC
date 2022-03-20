using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Services
{
    public interface ISensorsReadingService
    {
        DataGenericResponse<List<SensorReadingsResponseViewModel>> GetSensorReadings(Guid deviceId, DateTime? from, DateTime? to, int pageNumber = 1, int pageSize = 10);
        GenericResponse CreateInvalidReading(CreateInvalidSensorReadingRequest request);
    }
}
