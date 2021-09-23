using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GroceryStoreAPI.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Extensions
{
    public static class HttpResponseExtensions
    {
        public static Task SendExceptionResultAsync(this HttpResponse response, ResponseInfo<object> data,
            HttpStatusCode statusCode)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            return response.WriteAsync(SerializeDataIntoJson(response, data), Encoding.UTF8);
        }

        private static string SerializeDataIntoJson(HttpResponse response, object data)
        {
            var options = response.HttpContext.RequestServices.GetService<JsonSerializerOptions>();

            return JsonSerializer.Serialize(data, options);
        }
    }
}