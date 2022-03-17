using SmartAC.Models.Data.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Data
{
    public interface IAlertDataService
    {
        public void CreateAlert(Alert alert);
        public void EditAlert(Alert alert);
        public Alert GetAlertById(Guid id);
        public IEnumerable<Alert> GetAlerts(Func<Alert, bool> expression);
    }
}
