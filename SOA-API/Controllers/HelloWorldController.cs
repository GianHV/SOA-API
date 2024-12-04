using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SOA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ApiControllerBase
    {
        [HttpGet]
        public ApiResponse Get()
        {
            var data = "Hello world";
            return APIResponse(data);
        }
    }
}
