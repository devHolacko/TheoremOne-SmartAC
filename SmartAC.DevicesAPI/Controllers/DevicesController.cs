using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC.DevicesAPI.Controllers.Base;
using SmartAC.Models.Interfaces.Services;

namespace SmartAC.DevicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : BaseAuthController
    {
        private readonly IDeviceService _deviceService;
        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
    }
}
