using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackServer.Repositories;
using BackServer.Services.Interfaces;
using Entity;
using Microsoft.Extensions.Logging;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsHandlersController : ControllerBase
    {
        private readonly ILogger<ProductsHandlersController> _logger;
        private readonly IProductService _service;

        public ProductsHandlersController(ILogger<ProductsHandlersController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("~/GetAllProducts")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _service.GetAll();
        }
        
        [HttpGet("~/GetProductsByHeadingOne")]
        public async Task<IEnumerable<Product>> GetProductsByHeadingOne(HeadingOne headingOne)
        {
            return await _service.GetByHeadingOne(headingOne);
        }
        
        [HttpGet("~/GetProductsByHeadingTwo")]
        public async Task<IEnumerable<Product>> GetProductsByHeadingTwo(HeadingTwo headingTwo)
        {
            return await _service.GetByHeadingTwo(headingTwo);
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