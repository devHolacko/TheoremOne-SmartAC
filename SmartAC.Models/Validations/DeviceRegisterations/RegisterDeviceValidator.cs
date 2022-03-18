using FluentValidation;
using SmartAC.Common;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Devices;
using SmartAC.Models.ViewModels.Requests.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.DeviceRegisterations
{
    public class RegisterDeviceValidator : AbstractValidator<RegisterDeviceRequest>
    {
        public RegisterDeviceValidator()
        {
            RuleFor(x => x.Serial).NotNull().NotEmpty().MaximumLength(32).WithMessage(ErrorCodesConsts.INVALID_SERIAL);
            RuleFor(x => x.Secret).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_SECRET);
            RuleFor(x => x.FirmwareVersion).NotNull().NotEmpty().Matches(RegexConsts.SEMANTIC_VERSIONING_REGEX);
        }
    }
}
