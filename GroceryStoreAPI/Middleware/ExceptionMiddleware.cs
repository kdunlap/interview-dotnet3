using System;
using System.Net;
using System.Threading.Tasks;
using GroceryStoreAPI.Extensions;
using GroceryStoreAPI.Responses;
using Microsoft.AspNetCore.Http;
// using Serilog;

namespace GroceryStoreAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next)//, ILogger logger)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ResponseInfo<object> result = null;
            HttpStatusCode statusCode;

            switch (exception)
            {
                default:
                    result = new ResponseInfo<object>(false);
                    result.AddMessage(null, exception.Message);
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            await context.Response.SendExceptionResultAsync(result, statusCode);
        }
    }
}