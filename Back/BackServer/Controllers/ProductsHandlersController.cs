using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackServer.Services.Interfaces;
using Entity;
using Microsoft.Extensions.Logging;
using NpgsqlDbExtensions.Enums;

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


        [HttpGet("~/GetAvailableProducts")]
        public async Task<IEnumerable<Product>> GetAvailableProducts()
        {
            return await _service.GetAvailable();
        }

        [HttpGet("~/GetProductByTitle")]
        public async Task<Product?> GetByTitle(string title)
        {
            return await _service.GetByTitle(title);
        }
        
        [HttpGet("~/GetProductBySubstring")]
        public async Task<IEnumerable<Product>> GetBySubstring(string substring)
        {
            return await _service.GetBySubstring(substring);
        }
        
        [HttpPost("~/GetProductBySubstrings")]
        public async Task<IEnumerable<Product>> GetBySubstrings(string[] substring)
        {
            return await _service.GetBySubstrings(substring);
        }

        [HttpPost("~/GetPageHeadingOne")]
        public async Task<IEnumerable<Product>> GetPageHeadingOne(string headingOneTitle,
            ProductOrders productOrder, Property[] requiredProperties, int pageNumber, int countElements)
        {
            return await _service.GetPageHeadingOne(headingOneTitle, productOrder,
                requiredProperties.ToDictionary(x => x.Title, x => x.Values.ToHashSet()), pageNumber, countElements);
        }

        [HttpPost("~/GetPageHeadingTwo")]
        public async Task<IEnumerable<Product>> GetPageHeadingTwo(string headingTwoTitle,
            ProductOrders productOrder, Property[] requiredProperties, int pageNumber, int countElements)
        {
            return await _service.GetPageHeadingTwo(headingTwoTitle, productOrder,
                requiredProperties.ToDictionary(x => x.Title, x => x.Values.ToHashSet()), pageNumber, countElements);
        }

        [HttpPost("~/GetPageHeadingThree")]
        public async Task<IEnumerable<Product>> GetPageHeadingThree(string headingThreeTitle,
            ProductOrders productOrder, Property[] requiredProperties, int pageNumber, int countElements)
        {
            return await _service.GetPageHeadingThree(headingThreeTitle, productOrder,
                requiredProperties.ToDictionary(x => x.Title, x => x.Values.ToHashSet()), pageNumber, countElements);
        }

        [HttpPost("~/GetCountPagesHeadingOne")]
        public async Task<int> GetCountPagesHeadingOne(string headingOneTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements)
        {
            return await _service.GetCountPagesHeadingOne(headingOneTitle, productOrder, reqProperties, countElements);
        }

        [HttpPost("~/GetCountPagesHeadingTwo")]
        public async Task<int> GetCountPagesHeadingTwo(string headingTwoTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements)
        {
            return await _service.GetCountPagesHeadingTwo(headingTwoTitle, productOrder, reqProperties, countElements);
        }

        [HttpPost("~/AddProduct")]
        public async Task<ObjectResult> AddProduct(Product product)
        {
            var success = await _service.Add(product);
            if (success)
                return StatusCode(200, "");

            return StatusCode(400, "");
        }

        [HttpDelete("~/DeleteProducts")]
        public async Task<StatusCodeResult> DeleteProduct(HashSet<string> productTitles)
        {
            var success = await _service.Delete(productTitles);
            if (success)
                return Ok();

            return BadRequest();
        }

        [HttpPost("~/UpdateProduct")]
        public async Task<StatusCodeResult> UpdateProduct(string oldProductTitle, Product product)
        {
            var success = await _service.Update(oldProductTitle, product);
            if (success)
                return Ok();

            return BadRequest();
        }
        
        [HttpPost("~/UpdateProductPopularity")]
        public async Task<StatusCodeResult> UpdateProductPopularity(string productTitle, int newPopularity)
        {
            var success = await _service.UpdatePopularity(productTitle, newPopularity);
            if (success)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("~/DeleteHeadingOneProducts")]
        public async Task<StatusCodeResult> DeleteHeadingOneProducts(string headingOneTitle)
        {
            var success = await _service.DeleteHeadingOneProducts(headingOneTitle);
            if (success)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("~/DeleteHeadingTwoProducts")]
        public async Task<StatusCodeResult> DeleteHeadingTwoProducts(string headingTwoTitle)
        {
            var success = await _service.DeleteHeadingTwoProducts(headingTwoTitle);
            if (success)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("~/DeleteHeadingThreeProducts")]
        public async Task<StatusCodeResult> DeleteHeadingThreeProducts(string headingThreeTitle)
        {
            var success = await _service.DeleteHeadingThreeProducts(headingThreeTitle);
            if (success)
                return Ok();

            return BadRequest();
        }
    }
}