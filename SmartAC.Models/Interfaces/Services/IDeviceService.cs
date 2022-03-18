﻿using SmartAC.Models.ViewModels.Requests.Devices;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Services
{
    public interface IDeviceService
    {
        GenericResponse Register(RegisterDeviceRequest request);
    }
}