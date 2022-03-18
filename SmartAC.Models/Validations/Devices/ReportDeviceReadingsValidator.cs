using FluentValidation;
using SmartAC.Models.Consts;
using SmartAC.Models.ViewModels.Requests.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.Devices
{
    public class ReportDeviceReadingsValidator : AbstractValidator<ReportDeviceReadingsRequest>
    {
        public ReportDeviceReadingsValidator()
        {
            RuleFor(x => x.DeviceId).NotEmpty().NotNull().WithMessage(ErrorCodesConsts.INVALID_DEVICE_ID);
            RuleFor(x => x.Readings).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.READINGS_EMPTY);
        }
    }
}
