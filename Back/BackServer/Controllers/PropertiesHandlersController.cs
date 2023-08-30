using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackServer.Services.Interfaces;
using Entity;
using Microsoft.Extensions.Logging;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertiesHandlersController : ControllerBase
    {
        private readonly ILogger<PropertiesHandlersController> _logger;
        private readonly IPropertyService _service;

        public PropertiesHandlersController(ILogger<PropertiesHandlersController> logger, IPropertyService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("~/GetAllPropertiesTitle")]
        public async Task<IEnumerable<string>> GetAllPropertiesTitle()
        {
            return await _service.GetAllTitles();
        }
        
        [HttpGet("~/GetPropertiesByHeadingOne")]
        public async Task<IEnumerable<Property>> GetPropertiesByHeadingOne(string headingOneTitle)
        {
            return await _service.GetByHeadingOne(headingOneTitle);
        }

        [HttpGet("~/GetPropertiesByHeadingTwo")]
        public async Task<IEnumerable<Property>> GetPropertiesByHeadingTwo(string headingTwoTitle)
        {
            return await _service.GetByHeadingTwo(headingTwoTitle);
        }
        
        [HttpGet("~/GetPropertiesByHeadingThree")]
        public async Task<IEnumerable<Property>> GetPropertiesByHeadingThree(string headingThreeTitle)
        {
            return await _service.GetByHeadingTwo(headingThreeTitle);
        }

        [HttpGet("~/GetAllPropertiesByProduct")]
        public async Task<IEnumerable<Property>> GetAllPropertiesByProduct(string productTitle)
        {
            return await _service.GetAllByProduct(productTitle);
        }
        
        [HttpGet("~/GetPriorityPropertiesByProduct")]
        public async Task<IEnumerable<Property>> GetPriorityPropertiesByProduct(string productTitle)
        {
            return await _service.GetPriorityByProduct(productTitle);
        }

        [HttpPost("~/AddProperty")]
        public async Task<StatusCodeResult> Add(string propertyTitle)
        {
            var success = await _service.AddProperty(propertyTitle);
            if (success)
                return Ok();
            return BadRequest();
        }
        
        [HttpPost("~/AddPropertyValues")]
        public async Task<bool> AddPropertyValue(string propertyTitle, string[] propertyValues)
        {
            return await _service.AddPropertyValue(propertyTitle, propertyValues);
        }

        [HttpDelete("~/DeleteProperty")]
        public async Task<StatusCodeResult> Delete(string propertyTitle)
        {
            var success = await _service.DeleteProperty(propertyTitle);
            if (success)
                return Ok();
            return BadRequest();
        }
        
        [HttpDelete("~/DeletePropertyValue")]
        public async Task<bool> DeletePropertyValue(string propertyTitle, string propertyValue)
        {
            return await _service.DeletePropertyValue(propertyTitle, propertyValue);
        }

        [HttpPost("~/UpdateProperty")]
        public async Task<StatusCodeResult> Update([FromQuery] Property oldProperty, Property property)
        {
            var success = await _service.UpdateProperty(oldProperty, property);
            if (success)
                return Ok();
            return BadRequest();
        }

        [HttpPost("~/AddProductPropertyValue")]
        public async Task<StatusCodeResult> AddProductPropertyValue(string productTitle, string propertyTitle,
            string propertyValue, bool isPriority)
        {
            var success = await _service.AddProductPropertyValue(productTitle, propertyTitle, propertyValue, isPriority);
            if (success)
                return Ok();
            return BadRequest();
        }
        
        [HttpPost("~/AddProductProperties")]
        public Task<bool[]> AddProductProperties(string productTitle, Property[] properties)
        {
            return _service.AddProductProperties(productTitle, properties);
        }
        

        [HttpDelete("~/DeleteProductPropertyValue")]
        public async Task<StatusCodeResult> DeleteProductPropertyValue(string productTitle, string propertyTitle,
            string propertyValue)
        {
            var success = await _service.DeleteProductPropertyValue(productTitle, propertyTitle, propertyValue);
            if (success)
                return Ok();
            return BadRequest();
        }

        [HttpPost("~/UpdateProductPropertyValue")]
        public async Task<StatusCodeResult> UpdateProductPropertyValue(string productTitle, string propertyTitle,
            string oldPropertyValue,
            string newPropertyValue)
        {
            var success =
                await _service.UpdateProductPropertyValue(productTitle, propertyTitle, oldPropertyValue,
                    newPropertyValue);
            if (success)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("~/DeleteAllProductProperties")]
        public async Task<StatusCodeResult> DeleteAllProductProperties(string productTitle)
        {
            var success = await _service.DeleteAllProductProperties(productTitle);
            if (success)
                return Ok();
            return BadRequest();
        }

        [HttpDelete("~/DeleteAllPropertyValues")]
        public async Task<StatusCodeResult> DeleteAllPropertyValues(string propertyTitle)
        {
            var success = await _service.DeleteAllPropertyValues(propertyTitle);
            if (success)
                return Ok();
            return BadRequest();
        }
        
        [HttpPost("~/AddFilterHeadingOne")]
        public async Task<bool> AddFilterHeadingOne(string propertyTitle, string headingOneTitle)
        {
            return await _service.AddFilterHeadingOne(propertyTitle, headingOneTitle);
        }

        [HttpDelete("~/DeleteHeadingOneFilter")]
        public async Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingOneTitle)
        {
            return await _service.DeleteHeadingOneFilter(propertyTitle, headingOneTitle);
        }

        [HttpDelete("~/DeleteAllHeadingOneFilters")]
        public async Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle)
        {
            return await _service.DeleteAllHeadingOneFilters(headingOneTitle);
        }

        [HttpPost("~/UpdateHeadingOneFilter")]
        public async Task<bool> UpdateHeadingOneFilter(string headingOneFilter, string oldPropertyTitle, string newPropertyTitle)
        {
            return await _service.UpdateHeadingOneFilter(headingOneFilter, oldPropertyTitle, newPropertyTitle);
        }

        [HttpPost("~/AddFilterHeadingTwo")]
        public async Task<bool> AddFilterHeadingTwo(string propertyTitle, string headingTwoTitle)
        {
            return await _service.AddFilterHeadingTwo(propertyTitle, headingTwoTitle);
        }

        [HttpDelete("~/DeleteHeadingTwoFilter")]
        public async Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTwoTitle)
        {
            return await _service.DeleteHeadingTwoFilter(propertyTitle, headingTwoTitle);
        }

        [HttpDelete("~/DeleteAllHeadingTwoFilters")]
        public async Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle)
        {
            return await _service.DeleteAllHeadingTwoFilters(headingTwoTitle);
        }

        [HttpPost("~/UpdateHeadingTwoFilter")]
        public async Task<bool> UpdateHeadingTwoFilter(string headingTwoFilter, string oldPropertyTitle, string newPropertyTitle)
        {
            return await _service.UpdateHeadingTwoFilter(headingTwoFilter, oldPropertyTitle, newPropertyTitle);
        }
    }
}