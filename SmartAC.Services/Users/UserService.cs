using AutoMapper;
using Microsoft.Extensions.Options;
using SmartAC.Common.Token;
using SmartAC.Models.Common;
using SmartAC.Models.Consts;
using SmartAC.Models.Data.Users;
using SmartAC.Models.Interfaces.Common;
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
        private readonly ICacheManager _cacheManager;
        private readonly User dummyUser;
        private readonly AppSettings appSettings;
        public UserService(IOptions<AppSettings> appSettings, ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            this.appSettings = appSettings.Value;

            dummyUser = new User
            {
                Id = Guid.Parse("47690c0e-7703-4230-8262-c143fc85f3eb"),
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

            if (request.UserName != dummyUser.UserName || request.Password != dummyUser.Password)
            {
                return response.CreateFailureResponse(ErrorCodesConsts.INVALID_USERNAME_PASSWORD);
            }
            string jwtToken = TokenHelper.GenerateJwtToken(appSettings.Secret, dummyUser.Id,CommonConsts.ISSUER_ADMIN_API);

            _cacheManager.Add(CommonConsts.TOKEN, jwtToken);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS, jwtToken);
        }

        public GenericResponse Logout()
        {
            GenericResponse response = new GenericResponse();
            string token = _cacheManager.Get(CommonConsts.TOKEN);
            if (string.IsNullOrWhiteSpace(token))
            {
                return response.CreateFailureResponse(ErrorCodesConsts.ALREADY_LOGGED_OUT);
            }

            _cacheManager.Remove(CommonConsts.TOKEN);

            return response.CreateSuccessResponse(ErrorCodesConsts.SUCCESS);
        }
    }
}
