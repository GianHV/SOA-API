using AutoMapper;
using Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOA_API.Models;
using SOA_API.Services;

namespace SOA_API.Controllers
{
    [Route("api/products")]
    [Authorize]
    [ApiController]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _productService.GetProducts();
            return APIResponse(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _productService.GetProduct(id);
            return APIResponse(data);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductDTO request)
        {
            Product product = _mapper.Map<Product>(request);
            var data = _productService.AddProduct(product);
            return APIResponse(data);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductPutDTO request)
        {
            Product product = _mapper.Map<Product>(request);
            _productService.EditProduct(product);
            return APIResponse(string.Empty);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return APIResponse(string.Empty);
        }
    }
}
