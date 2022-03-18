using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Responses.Base
{
    public class DataGenericResponse<T> : GenericResponse
        where T : class, new()
    {
        public T Data { get; set; }
    }
}
