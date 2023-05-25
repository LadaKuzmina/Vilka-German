using Microsoft.AspNetCore.Mvc;
using BackServer.Repositories;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeadingsHandlersController : ControllerBase
    {
        private readonly ILogger<HeadingsHandlersController> _logger;

        public HeadingsHandlersController(ILogger<HeadingsHandlersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("~/GetAllHeadingsOne")]
        public async Task<JsonContent> GetAllHeadingsOne(JsonContent json)
        {
            throw new NotImplementedException();
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