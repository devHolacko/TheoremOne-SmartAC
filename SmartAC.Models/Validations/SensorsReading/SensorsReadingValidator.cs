using SmartAC.Models.Enums;
using SmartAC.Models.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.SensorsReading
{
    public class SensorsReadingValidator
    {
        public static bool Validate(decimal value, AlertType alertType)
        {
            ISensorValidator validator;
            switch (alertType)
            {
                case AlertType.Temperature:
                    validator = new TemperatureReadingValidator();
                    break;
                case AlertType.CarbonMonoxide:
                    validator = new CarbonMonoxideValidator();
                    break;
                case AlertType.Humidity:
                    validator = new HumidityValidator();
                    break;
                default:
                    return false;
            }

            return validator.Validate(value);
        }
    }
}
