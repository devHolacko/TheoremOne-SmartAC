using FluentValidation;
using SmartAC.Models.Consts;
using SmartAC.Models.ViewModels.Requests.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.SensorsReading
{
    public class CreateInvalidSensorReadingValidator : AbstractValidator<CreateInvalidSensorReadingRequest>
    {
        public CreateInvalidSensorReadingValidator()
        {
            RuleFor(c => c.DeviceId).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_DEVICE_ID);
            RuleFor(c => c.Data).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_REQUEST);
        }
    }
}
