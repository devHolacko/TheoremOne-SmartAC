using SmartAC.Models.ViewModels.Requests.Users;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Models.Interfaces.Services
{
    public interface IUserService
    {
        DataGenericResponse<string> Login(LoginRequest request);
        GenericResponse Logout(string token);
    }
}
