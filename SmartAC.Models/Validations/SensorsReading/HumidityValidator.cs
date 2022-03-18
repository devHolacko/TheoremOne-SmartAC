using SmartAC.Models.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.SensorsReading
{
    internal class HumidityValidator : ISensorValidator
    {
        public bool Validate(decimal value)
        {
            return (value >= 0 && value <= 100);
        }
    }
}
