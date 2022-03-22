using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SmartAC.DevicesAPI.Controllers;
using SmartAC.Models.Interfaces.Services;
using SmartAC.Models.ViewModels.Requests.Devices;

namespace SmartAC.API.Test
{
    public class DevicesApiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_RegisterDevice_Succeed()
        {
            //Arrange
            Mock<IDeviceService> deviceServiceMock = new Mock<IDeviceService>();
            Mock<ActionContext> requestActionContext = new Mock<ActionContext>();
            DevicesController devicesController = new DevicesController(deviceServiceMock.Object);
            RegisterDeviceRequest registerDeviceRequest = new RegisterDeviceRequest
            {
                FirmwareVersion = "2.0.0",
                Secret = "testsecret",
                Serial = "testserial"
            };

            //Act
            var response = devicesController.Register(registerDeviceRequest);


            //Assert
            Assert.AreEqual(response.GetType(), typeof(OkObjectResult));
        }
    }
}