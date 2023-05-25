using Microsoft.AspNetCore.Mvc;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HandlersController : ControllerBase
    {
        private readonly ILogger<HandlersController> _logger;

        public HandlersController(ILogger<HandlersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHandler")]
        public async Task<JsonContent> Get(JsonContent json)
        {
            throw new NotImplementedException();
        }

        [HttpPost(Name = "PostHandler")]
        public async Task<JsonContent> Post(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete(Name = "DeleteHandler")]
        public async Task<JsonContent> Delete(JsonContent json)
        {
            throw new NotImplementedException();
        }
    }
}