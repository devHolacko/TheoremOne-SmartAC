using AutoMapper;
using SmartAC.Common.Common;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.SensorsReading;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
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
        private readonly IInvalidSensorsReadingDataService _invalidSensorsReadingDataService;
        private readonly IAlertDataService _alertDataService;
        private readonly IMapper _mapper;
        public SensorsReadingService(ISensorsReadingDataService sensorsReadingDataService, IMapper mapper, IAlertDataService alertDataService, IInvalidSensorsReadingDataService invalidSensorsReadingDataService)
        {
            _sensorsReadingDataService = sensorsReadingDataService;
            _mapper = mapper;
            _alertDataService = alertDataService;
            _invalidSensorsReadingDataService = invalidSensorsReadingDataService;
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

        public GenericResponse CreateInvalidReading(CreateInvalidSensorReadingRequest request)
        {
            GenericResponse response = new GenericResponse();
            CreateInvalidSensorReadingValidator validator = new CreateInvalidSensorReadingValidator();

            if (request == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_REQUEST);
            }

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return response.CreateFailureResponse(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
            }

            if (request.Data.Length > 500)
            {
                request.Data = request.Data.Substring(0, 500);
            }

            InvalidSensorReading reading = _mapper.Map<InvalidSensorReading>(request);

            _invalidSensorsReadingDataService.CreateInvalidReading(reading);

            int deviceInvalidReadingsCount = _invalidSensorsReadingDataService.GetInvalidReadings(c => c.DeviceId == request.DeviceId).Count();
            if (deviceInvalidReadingsCount > 500)
            {
                bool deviceHasAlert = _alertDataService.GetAlerts(c => c.DeviceId == request.DeviceId && c.Type == Models.Enums.AlertType.InvalidData && c.ResolutionStatus != Models.Enums.AlertResolutionStatus.Resolved).Any();
                if (!deviceHasAlert)
                {
                    _alertDataService.CreateAlert(new Alert
                    {
                        AlertDate = DateTime.UtcNow,
                        DeviceId = request.DeviceId,
                        Message = "Device sending unintelligible data",
                    });
                }
            }

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }

        public DataGenericResponse<List<BucketViewModel>> AggregateSensorReadingByDateRange(Guid deviceId, DateTime fromDate, DateTime toDate)
        {
            DataGenericResponse<List<BucketViewModel>> response = new DataGenericResponse<List<BucketViewModel>>();
            var query = _sensorsReadingDataService.GetSensorReadings(c => c.DeviceId == deviceId);

            DateTime todayDate = DateTime.UtcNow;

            query = query.Where(c => c.RecordedAt >= fromDate && c.RecordedAt <= toDate);

            int rangeHours = (toDate - todayDate).Hours;
            int numberOfDays = (toDate - todayDate).Days;

            

            var (Hours, Buckets) = BucketsCalculationHelper.GetRequiredBucketsNumber(numberOfDays);

            if (Hours == 0 && Buckets == 0)
            {
                var bucketSize = rangeHours / 28;
                if (bucketSize > 24)
                {
                    Hours = numberOfDays / 30;
                    Buckets = 30;
                }
                else
                {
                    Hours = bucketSize;
                    Buckets = 28;
                }
            }

            var dateRanges = BucketsCalculationHelper.CalculateDateRanges(Buckets, Hours, fromDate);

            List<BucketViewModel> buckets = new List<BucketViewModel>();

            foreach (var dateRange in dateRanges)
            {
                var dateRangeQuery = query.Where(c => c.RecordedAt >= dateRange.fromDateItem && c.RecordedAt <= dateRange.toDateItem).ToList();

                decimal maxCO = dateRangeQuery.Max(c => c.CarbonMonoxide);
                decimal maxHumidity = dateRangeQuery.Max(c => c.Humidity);
                decimal maxTemperature = dateRangeQuery.Max(c => c.Temperature);

                decimal minCO = dateRangeQuery.Min(c => c.CarbonMonoxide);
                decimal minHumidity = dateRangeQuery.Min(c => c.Humidity);
                decimal minTemperature = dateRangeQuery.Min(c => c.Temperature);

                decimal avgCO = dateRangeQuery.Average(c => c.CarbonMonoxide);
                decimal avgHumidity = dateRangeQuery.Average(c => c.Humidity);
                decimal avgTemperature = dateRangeQuery.Average(c => c.Temperature);

                decimal firstCO = dateRangeQuery.OrderBy(c => c.CarbonMonoxide).FirstOrDefault().CarbonMonoxide;
                decimal firstHumidity = dateRangeQuery.OrderBy(c => c.Humidity).FirstOrDefault().Humidity;
                decimal firstTemperature = dateRangeQuery.OrderBy(c => c.Temperature).FirstOrDefault().Temperature;


                decimal lastCO = dateRangeQuery.OrderByDescending(c => c.CarbonMonoxide).FirstOrDefault().CarbonMonoxide;
                decimal lastHumidity = dateRangeQuery.OrderByDescending(c => c.Humidity).FirstOrDefault().Humidity;
                decimal lastTemperature = dateRangeQuery.OrderByDescending(c => c.Temperature).FirstOrDefault().Temperature;


                BucketItemValueViewModel coItem = new BucketItemValueViewModel
                {
                    First = firstCO,
                    Last = lastCO,
                    Average = avgCO,
                    Min = minCO,
                    Max = maxCO,
                };

                BucketItemValueViewModel humidityItem = new BucketItemValueViewModel
                {
                    First = firstHumidity,
                    Last = lastHumidity,
                    Average = avgHumidity,
                    Min = minHumidity,
                    Max = maxHumidity,
                };

                BucketItemValueViewModel temperatureItem = new BucketItemValueViewModel
                {
                    First = firstTemperature,
                    Last = lastTemperature,
                    Average = avgTemperature,
                    Min = minTemperature,
                    Max = maxTemperature,
                };

                buckets.Add(new BucketViewModel
                {
                    CarbonMonoxide = coItem,
                    Humidity = humidityItem,
                    Temperature = temperatureItem,
                });


            }

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, buckets);
        }


    }
}
