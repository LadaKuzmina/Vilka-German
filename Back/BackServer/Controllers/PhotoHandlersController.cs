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
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    public class PhotoHandlersController : ControllerBase
    {
        private readonly ILogger<HeadingsHandlersController> _logger;
        private readonly IPhotoService _service;

        public PhotoHandlersController(ILogger<HeadingsHandlersController> logger, IPhotoService service)
        {
            _logger = logger;
            _service = service;
        }
        
        [HttpGet("~/GetProductPrimaryPhoto")]
        public async Task<ObjectResult> GetPrimaryProductPhoto(string productTitle)
        {
            try
            {
                var t = await _service.GetPrimaryProductPhoto(productTitle);
                return StatusCode(200, t);
            }
            catch (ArgumentException e)
            {
                return StatusCode(204, null);
            }
        }

        [HttpGet("~/GetProductAllPhoto")]
        public async Task<ObjectResult> GetAllProductPhoto(string productTitle)
        {
            try
            {
                var t = await _service.GetAllProductPhoto(productTitle);
                return StatusCode(200, t);
            }
            catch (ArgumentException e)
            {
                return StatusCode(204, null);
            }
        }
        
        [HttpGet("~/GetProjectPrimaryPhoto")]
        public async Task<ObjectResult> GetPrimaryProjectPhoto(string projectTitle)
        {
            try
            {
                var t = await _service.GetPrimaryProjectPhoto(projectTitle);
                return StatusCode(200, t);
            }
            catch (ArgumentException e)
            {
                return StatusCode(204, null);
            }
        }

        [HttpGet("~/GetProjectAllPhoto")]
        public async Task<ObjectResult> GetAllProjectPhoto(string projectTitle)
        {
            try
            {
                var t = await _service.GetAllProjectPhoto(projectTitle);
                return StatusCode(200, t);
            }
            catch (ArgumentException e)
            {
                return StatusCode(204, null);
            }
        }
        
        [HttpPost("~/AddProductPhoto")]
        public async Task<bool> AddProductPhoto(string productTitle, string imageRef, bool isPriority)
        {
            return await _service.AddProductPhoto(productTitle, imageRef, isPriority);
        }

        [HttpDelete("~/DeleteProductPhoto")]
        public async Task<bool> DeleteProductPhoto(string productTitle, string imageRef)
        {
            return await _service.DeleteProductPhoto(productTitle, imageRef);
        }

        [HttpPost("~/UpdateProductPhoto")]
        public async Task<bool> UpdateProductPhoto(string productTitle, string oldImageRef, string newImageRef, bool isPriority)
        {
            return await _service.UpdateProductPhoto(productTitle, oldImageRef, newImageRef, isPriority);
        }

        [HttpPost("~/AddProjectPhoto")]
        public async Task<bool> AddProjectPhoto(string projectTitle, string imageRef, bool isPriority)
        {
            return await _service.AddProjectPhoto(projectTitle, imageRef, isPriority);
        }

        [HttpDelete("~/DeleteProjectPhoto")]
        public async Task<bool> DeleteProjectPhoto(string projectTitle, string imageRef)
        {
            return await _service.DeleteProjectPhoto(projectTitle, imageRef);
        }

        [HttpPost("~/UpdateProjectPhoto")]
        public async Task<bool> UpdateProjectPhoto(string projectTitle, string oldImageRef, string newImageRef, bool isPriority)
        {
            return await _service.UpdateProjectPhoto(projectTitle, oldImageRef, newImageRef, isPriority);
        }
    }
}