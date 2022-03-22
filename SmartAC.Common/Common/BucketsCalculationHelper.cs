using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Common.Common
{
    public class BucketsCalculationHelper
    {
        public static (int Hours, int Buckets) GetRequiredBucketsNumber(int numberOfDays)
        {
            switch (numberOfDays)
            {
                case 1:
                    return (1, 24);
                case 7:
                    return (6, 28);
                case 14:
                    return (12, 28);
                case 21:
                    return (16, 28);
                case 30:
                    return (24, 30);
                case 90:
                    return (72, 30);
                case 180:
                    return (144, 30);
                default:
                    //var bucketSize = numberOfDays / 28;
                    //if (bucketSize < 24)
                    //    return (bucketSize, 1);
                    //bucketSize = numberOfDays / 30;
                    //return (bucketSize, 30);
                    return (0, 0);
            }
        }

        public static List<(DateTime fromDateItem, DateTime toDateItem)> CalculateDateRanges(int numberOfBuckets, int hours, DateTime fromDate)
        {
            List<(DateTime fromDateItem, DateTime toDateItem)> datesList = new()
            {
                (fromDate, fromDate.AddHours(hours))
            };

            for (int i = 0; i <= numberOfBuckets; i++)
            {
                DateTime newFromDate = datesList[i].toDateItem.AddSeconds(1);
                DateTime newToDate = newFromDate.AddHours(hours);
                datesList.Add((newFromDate, newToDate));
            }

            return datesList;
        }
    }
}
