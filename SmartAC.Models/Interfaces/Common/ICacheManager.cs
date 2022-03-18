using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Common
{
    public interface ICacheManager
    {
        void Add(string key, string value);
        string Get(string key);
        void Remove(string key);
    }
}
