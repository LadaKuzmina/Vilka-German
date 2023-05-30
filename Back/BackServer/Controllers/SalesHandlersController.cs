using Microsoft.AspNetCore.Mvc;
using BackServer.Repositories;
using Microsoft.Extensions.Logging;

namespace BackServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesHandlersController : ControllerBase
    {
        private readonly ILogger<SalesHandlersController> _logger;

        public SalesHandlersController(ILogger<SalesHandlersController> logger)
        {
            _logger = logger;
        }
    }
}