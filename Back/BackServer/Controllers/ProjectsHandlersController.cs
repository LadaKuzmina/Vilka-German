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
    public class ProjectsHandlersController : ControllerBase
    {
        private readonly ILogger<ProjectsHandlersController> _logger;

        public ProjectsHandlersController(ILogger<ProjectsHandlersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("~/GetAllProjects")]
        public async Task<JsonContent> GetAllProjects(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost("~/AddProject")]
        public async Task<JsonContent> AddProject(JsonContent json)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("~/DeleteProject")]
        public async Task<JsonContent> DeleteProject(JsonContent json)
        {
            throw new NotImplementedException();
        }
    }
}