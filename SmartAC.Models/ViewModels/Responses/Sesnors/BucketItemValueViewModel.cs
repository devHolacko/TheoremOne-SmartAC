using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Responses.Sesnors
{
    public class BucketItemValueViewModel
    {
        public decimal First { get; set; }
        public decimal Last { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Average { get; set; }
    }
}
