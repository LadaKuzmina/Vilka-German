using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BackServer.Contexts;
using Microsoft.AspNetCore.Mvc;
using BackServer.Repositories;
using Microsoft.Extensions.Logging;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeadingsHandlersController : ControllerBase
    {
        private readonly ILogger<HeadingsHandlersController> _logger;
        private readonly TestContext _context;

        public HeadingsHandlersController(ILogger<HeadingsHandlersController> logger, TestContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("~/GetAllHeadingsOne")]
        public async Task<IEnumerable<Entity.HeadingOne>> GetAllHeadingsOne()
        {
            var rep = new HeadersRepositoryDb(_context);
            return rep.GetAllHeadingsOne();
        }
        
        [HttpGet("~/GetAllHeadingsTwo")]
        public async Task<JsonContent> GetAllHeadingsTwo(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet("~/GetHeadingsTwoByHeadingsOne")]
        public async Task<JsonContent> GetHeadingsTwoByHeadingsOne(JsonContent json)
        {
            throw new NotImplementedException();
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