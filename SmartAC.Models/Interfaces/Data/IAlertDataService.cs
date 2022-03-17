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
        public void CreateDevice(Alert device);
        public void EditDevice(Alert device);
        public Alert GetDeviceById(Guid id);
        public IEnumerable<Alert> GetDevices(Expression<Func<Alert, bool>> expression);
    }
}
