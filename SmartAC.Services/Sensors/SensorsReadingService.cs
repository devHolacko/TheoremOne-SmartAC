using AutoMapper;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Responses.Base;
using SmartAC.Models.ViewModels.Responses.Sesnors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Sensors
{
    public class SensorsReadingService : ISensorsReadingService
    {
        private readonly ISensorsReadingDataService _sensorsReadingDataService;
        private readonly IMapper _mapper;
        public SensorsReadingService(ISensorsReadingDataService sensorsReadingDataService, IMapper mapper)
        {
            _sensorsReadingDataService = sensorsReadingDataService;
            _mapper = mapper;
        }

        public DataGenericResponse<List<SensorReadingsResponseViewModel>> GetSensorReadings(Guid deviceId, DateTime? from, DateTime? to, int pageNumber = 1, int pageSize = 10)
        {
            DataGenericResponse<List<SensorReadingsResponseViewModel>> response = new DataGenericResponse<List<SensorReadingsResponseViewModel>>();
            IEnumerable<SensorsReading> query = _sensorsReadingDataService.GetSensorReadings(c => c.DeviceId == deviceId);

            if (from.HasValue && to.HasValue)
            {
                query = query.Where(c => c.RecordedAt >= from.Value.ToUniversalTime() && c.RecordedAt <= to.Value.ToUniversalTime());
            }
            else if (from.HasValue)
            {
                query = query.Where(c => c.RecordedAt >= from.Value.ToUniversalTime());
            }
            else if (to.HasValue)
            {
                query = query.Where(c => c.RecordedAt <= to.Value.ToUniversalTime());
            }

            query = query.Skip((pageSize - 1) * pageNumber).Take(pageSize);

            if (!query.Any())
            {
                return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, new List<SensorReadingsResponseViewModel>());
            }

            var readingsList = query.ToList();

            List<SensorReadingsResponseViewModel> mappedReadings = _mapper.Map<List<SensorReadingsResponseViewModel>>(readingsList);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, mappedReadings);
        }
    }
}
