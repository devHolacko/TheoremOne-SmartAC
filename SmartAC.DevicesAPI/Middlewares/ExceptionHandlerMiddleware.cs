using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmartAC.Models.Consts;
using SmartAC.Models.ViewModels.Responses.Base;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.DevicesAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                try
                {
                    context.Request.EnableBuffering();
                    var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    string requestBody = Encoding.UTF8.GetString(buffer);
                }
                finally
                {
                    context.Request.Body.Position = 0;
                }
                await _next(context);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = JsonConvert.SerializeObject(
                new GenericResponse()
                {
                    Message = ErrorCodesConsts.ERROR_OCCURED,
                    Success = false
                },
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            );
            Console.WriteLine(exception.ToString());

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(result);
        }
    }
}
