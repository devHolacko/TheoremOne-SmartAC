using FluentValidation;
using SmartAC.Models.Consts;
using SmartAC.Models.ViewModels.Requests.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.Alerts
{
    public class CreateAlertValidator : AbstractValidator<CreateAlertRequest>
    {
        public CreateAlertValidator()
        {
            RuleFor(c => c.DeviceId).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_DEVICE_ID);
            RuleFor(c => c.Message).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_MESSAGE);
            RuleFor(c => c.Code).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_CODE);
            RuleFor(c => c.AlertDate).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_ALERT_DATE);
            RuleFor(c => c.SensorReadingId).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_SENSOR_READING);
        }
    }
}
