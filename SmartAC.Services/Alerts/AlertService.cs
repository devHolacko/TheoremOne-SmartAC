using AutoMapper;
using SmartAC.Models.Interfaces.Data;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Alerts;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Alerts
{
    public class AlertService : IAlertService
    {
        private readonly IMapper _mapper;
        private readonly IAlertDataService _alertDataService;
        public AlertService(IMapper mapper, IAlertDataService alertDataService)
        {
            _mapper = mapper;
            _alertDataService = alertDataService;
        }
        public GenericResponse Create(CreateAlertRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
