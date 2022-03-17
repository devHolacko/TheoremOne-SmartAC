using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Interfaces.Common;
using SmartAC.Models.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Data
{
    public class AlertDataService : IAlertDataService
    {
        private readonly IGenericRepository<Alert> _alertsRepository;
        public AlertDataService(IGenericRepository<Alert> alertsRepository)
        {
            _alertsRepository = alertsRepository;
        }
        public void CreateAlert(Alert alert)
        {
            _alertsRepository.Insert(alert);
        }

        public void EditAlert(Alert alert)
        {
            _alertsRepository.Update(alert);
        }

        public Alert GetAlertById(Guid id)
        {
            return _alertsRepository.Get(id);
        }

        public IEnumerable<Alert> GetAlerts(Func<Alert, bool> expression)
        {
            return _alertsRepository.GetAll().Where(expression);
        }
    }
}
