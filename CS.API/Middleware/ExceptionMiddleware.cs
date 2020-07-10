using CS.API.ViewModels;
using CS.Core.Enums;
using CS.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CS.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int status = (int)HttpStatusCode.InternalServerError;

            if (ex is BadRequestException) status = (int)ErrorEnums.BadRequest;
            if (ex is UnAuthorizedException) status = (int)ErrorEnums.Unauthorized;
            if (ex is DuplicateException) status = (int)ErrorEnums.DuplicateUserName;

            var result = JsonConvert.SerializeObject(new ResponseDto { Code = status, Message = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(result);
        }
    }
}
