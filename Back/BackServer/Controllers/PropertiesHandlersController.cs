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
    public class PropertiesHandlersController : ControllerBase
    {
        private readonly ILogger<PropertiesHandlersController> _logger;

        public PropertiesHandlersController(ILogger<PropertiesHandlersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("~/GetPropertiesByHeadingOne")]
        public async Task<JsonContent> GetPropertiesByHeadingOne(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("~/GetPropertiesByHeadingTwo")]
        public async Task<JsonContent> GetPropertiesByHeadingTwo(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("~/GetPropertiesByProduct")]
        public async Task<JsonContent> GetPropertiesByProduct(JsonContent json)
        {
            throw new NotImplementedException();
        }
    }
}