using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Common
{
    internal interface ISensorValidator
    {
        bool Validate(decimal value);
    }
}
