using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BackServer.Contexts;
using Microsoft.AspNetCore.Mvc;
using BackServer.Repositories;
using BackServer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeadingsHandlersController : ControllerBase
    {
        private readonly ILogger<HeadingsHandlersController> _logger;
        private readonly IHeadersService _service;

        public HeadingsHandlersController(ILogger<HeadingsHandlersController> logger, IHeadersService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("~/GetAllHeadingsOne")]
        public async Task<IEnumerable<Entity.HeadingOne>> GetAllHeadingsOne()
        {
            await Task.Delay(5000);
            return await _service.GetAllHeadingsOne();
        }
        
        [HttpGet("~/GetAllHeadingsTwo")]
        public async Task<IEnumerable<Entity.HeadingTwo>> GetAllHeadingsTwo()
        {
            return await _service.GetAllHeadingsTwo();
        }
        
        [HttpGet("~/GetHeadingsTwoByHeadingsOne")]
        public async Task<IEnumerable<Entity.HeadingTwo>> GetHeadingsTwoByHeadingsOne(string headingOneTitle)
        {
            return await _service.GetHeadingsTwoByHeadingsOne(headingOneTitle);
        }

        [HttpPost("~/AddHeadingOne")]
        public async Task<JsonContent> AddHeadingOne(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("~/AddHeadingTwo")]
        public async Task<JsonContent> AddHeadingTwo(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("~/DeleteHeadingOne")]
        public async Task<JsonContent> DeleteHeadingOne(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("~/DeleteHeadingTwo")]
        public async Task<JsonContent> DeleteHeadingTwo(JsonContent json)
        {
            throw new NotImplementedException();
        }
    }
}