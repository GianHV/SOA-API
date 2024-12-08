using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resource3.Services.Internal;

namespace Resource3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductReportController : ApiControllerBase
    {
        private readonly IProductReportService _productReportService;
        public ProductReportController(IProductReportService productReportService)
        {
            _productReportService = productReportService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _productReportService.GetProductReports();
            return APIResponse(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _productReportService.GetProductReport(id);
            return APIResponse(data);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productReportService.DeleteProductReport(id);
            return APIResponse(string.Empty);
        }
    }
}
