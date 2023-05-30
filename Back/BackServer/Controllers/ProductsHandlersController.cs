using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackServer.Repositories;
using Microsoft.Extensions.Logging;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsHandlersController : ControllerBase
    {
        private readonly ILogger<ProductsHandlersController> _logger;

        public ProductsHandlersController(ILogger<ProductsHandlersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("~/GetAllProducts")]
        public async Task<JsonContent> GetAllProducts(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("~/GetProductsByHeadingOne")]
        public async Task<JsonContent> GetProductsByHeadingOne(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("~/GetProductsByHeadingTwo")]
        public async Task<JsonContent> GetProductsByHeadingTwo(JsonContent json)
        {
            throw new NotImplementedException();
        }

        [HttpPost("~/AddProduct")]
        public async Task<JsonContent> AddProduct(JsonContent json)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("~/DeleteProduct")]
        public async Task<JsonContent> DeleteProduct(JsonContent json)
        {
            throw new NotImplementedException();
        }
    }
}