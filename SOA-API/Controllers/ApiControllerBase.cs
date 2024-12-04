using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SOA_API.Controllers
{
    public class ApiControllerBase
    {
        protected ApiResponse APIResponse<T>(T value)
        {
            ApiResponse res = new();

            if (value == null)
            {
                res.StatusCode = HttpStatusCode.BadRequest;
                res.IsSuccess = false;
                res.ErrorMessages = new List<string>() { "Please try again!"};
            }
            else
            {
                res.StatusCode = HttpStatusCode.OK;
                res.IsSuccess = true;
                res.Result = value;
            }

            return res;
        }
    }
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object? Result { get; set; }
    }
}
