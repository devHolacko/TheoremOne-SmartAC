using FluentValidation;
using SmartAC.Models.Consts;
using SmartAC.Models.ViewModels.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Validations.Users
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_USERNAME);
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(ErrorCodesConsts.INVALID_PASSWORD);
        }
    }
}
