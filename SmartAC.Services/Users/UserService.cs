using AutoMapper;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Users;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.Validations.Users;
using SmartAC.Models.ViewModels;
using SmartAC.Models.ViewModels.Requests.Users;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly User dummyUser;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
            dummyUser = new User
            {
                Id = Guid.NewGuid(),
                Active = true,
                Email = "test@gmail.com",
                FirstName = "Michael",
                LastName = "Jordan",
                UserName = "mjordan",
                Password = "123456"
            };
        }

        public DataGenericResponse<string> Login(LoginRequest request)
        {
            DataGenericResponse<string> response = new DataGenericResponse<string>();
            LoginValidator validator = new LoginValidator();
            if (request == null)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_REQUEST);
            }

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return response.CreateFailureResponse(validationResult.Errors.Select(c => c.ErrorMessage).ToArray());
            }

            if(request.UserName != dummyUser.UserName || request.Password != dummyUser.Password)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_USERNAME_PASSWORD);
            }

            return null;
        }

        public GenericResponse Logout(string token)
        {
            throw new NotImplementedException();
        }
    }
}
