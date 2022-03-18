using SmartAC.Models.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.SensorsReading
{
    internal class TemperatureReadingValidator : ISensorValidator
    {
        public bool Validate(decimal value)
        {
            return (value >= -30 && value <= 100);
        }
    }
}
