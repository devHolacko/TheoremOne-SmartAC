using SmartAC.Models.ViewModels.Responses.Base;
using System;

namespace SmartAC.Models.ViewModels
{
    public static class ViewModelsExtensions
    {
        public static DataGenericResponse<T> CreateSuccessResponse<T>(this DataGenericResponse<T> response, string message, T data)
        {
            response.Success = true;
            response.Message = message;
            response.Data = data;

            return response;
        }
        public static DataGenericResponse<T> CreateSuccessResponse<T>(this DataGenericResponse<T> response, string[] messages, T data)
        {
            response.Success = true;
            response.Message = string.Join(Environment.NewLine, messages);
            response.Data = data;

            return response;
        }
        public static DataGenericResponse<T> CreateFailureResponse<T>(this DataGenericResponse<T> response, string message)
        {
            response.Success = false;
            response.Message = message;

            return response;
        }
        public static DataGenericResponse<T> CreateFailureResponse<T>(this DataGenericResponse<T> response, string[] messages)
        {
            response.Success = false;
            response.Message = string.Join(Environment.NewLine, messages);

            return response;
        }

        public static GenericResponse CreateSuccessResponse(this GenericResponse response, string message)
        {
            response.Success = true;
            response.Message = message;

            return response;
        }
        public static GenericResponse CreateFailureResponse(this GenericResponse response, string message)
        {
            response.Success = false;
            response.Message = message;

            return response;
        }

        public static GenericResponse CreateSuccessResponse(this GenericResponse response, string[] messages)
        {
            response.Success = true;
            response.Message = string.Join(Environment.NewLine, messages);

            return response;
        }
        public static GenericResponse CreateFailureResponse(this GenericResponse response, string[] messages)
        {
            response.Success = false;
            response.Message = string.Join(Environment.NewLine, messages);

            return response;
        }
    }
}
