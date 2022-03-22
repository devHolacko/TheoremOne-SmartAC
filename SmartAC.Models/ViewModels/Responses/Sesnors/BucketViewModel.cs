using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.ViewModels.Responses.Sesnors
{
    public class BucketViewModel
    {
        public BucketItemValueViewModel CarbonMonoxide { get; set; }
        public BucketItemValueViewModel Humidity { get; set; }
        public BucketItemValueViewModel Temperature { get; set; }
    }
}
