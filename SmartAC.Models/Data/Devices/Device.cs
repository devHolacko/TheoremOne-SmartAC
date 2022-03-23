using SmartAC.Models.Data.Alerts;
using SmartAC.Models.Data.Base;
using SmartAC.Models.Data.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Data.Devices
{
    public class Device : BaseAuditedEntity
    {
        public string Serial { get; set; }
        public string Secret { get; set; }

        public virtual IEnumerable<Alert> Alerts { get; set; }
        public virtual IEnumerable<DeviceRegisteration> DeviceRegisterations { get; set; }
        public virtual IEnumerable<SensorsReading> DeviceSensorReadings { get; set; }
        public virtual IEnumerable<InvalidSensorReading> DeviceInvalidReadings { get; set; }

        public static List<Device> GetSeedingData()
        {
            return new List<Device>
            {
                new Device{ Id = Guid.Parse("7997cfa3-99b2-40f9-ae97-18efdd6b348d"),Serial="HEP10sO7XlOVxFHH3N6CeoOcYzp1H",Secret="p19fym3p3u4dhmw2e6r1m34ulj0bsf83"},
                new Device{ Id = Guid.Parse("ef2930a0-40e8-4f19-9ad1-cbd9ac639c27"),Serial="3aW93UxRg1nPvKsLGICwc73PX3pLL43s",Secret="f92o5utv1xj4qo6wn990cxcuz6jizekx"},
                new Device{ Id = Guid.Parse("c7b64f64-59ac-47b9-8d97-e1d21cec4406"),Serial="tmKj7i6wTGO4MZ9WteG0AEw8z0raXn",Secret="iv10ytx4ax397beocgpvghu0txr2yo5r"},
                new Device{ Id = Guid.Parse("681378c9-48c1-44d6-a4dd-00a498073cc7"),Serial="R7v5qusR5QLYaZw7AMcb3W5tNkTuUgE",Secret="41511oh5lqerycl8g5u0db7vy4qh4jge"},
                new Device{ Id = Guid.Parse("ba35fd57-1831-43b7-99b7-9dbd912fbde2"),Serial="iO5FKAwUYW3m5jew7KTDMe7JuCnuBq",Secret="u54wpi8xphnn6p50yt3vbkfadnkcn9ld"}
            };
        }
    }
}
