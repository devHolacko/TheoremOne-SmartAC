﻿using AutoMapper;
using SmartAC.Models.Data.Sensors;
using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Mappings
{
    public class SensorReadingMapping : Profile
    {
        public SensorReadingMapping()
        {
            CreateMap(typeof(SensorsReading), typeof(SensorReadingViewModel)).ReverseMap();
        }
    }
}