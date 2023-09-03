using Microsoft.AspNetCore.Mvc;
using BackServer.Services.Interfaces;


namespace BackServer.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    public class ImageHandlersController : ControllerBase
    {
        private readonly ILogger<HeadingsHandlersController> _logger;
        private readonly IPhotoService _service;

        public ImageHandlersController(ILogger<HeadingsHandlersController> logger, IPhotoService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("~/GetProductPrimaryPhoto")]
        public async Task<ObjectResult> GetPrimaryProductPhoto(string productTitle)
        {
            try
            {
                var photo = await _service.GetPrimaryProductPhoto(productTitle);
                return StatusCode(200, photo);
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
                var allProductPhoto = await _service.GetAllProductPhoto(productTitle);
                return StatusCode(200, allProductPhoto);
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
                var photo = await _service.GetPrimaryProjectPhoto(projectTitle);
                return StatusCode(200, photo);
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
                var allProjectPhoto = await _service.GetAllProjectPhoto(projectTitle);
                return StatusCode(200, allProjectPhoto);
            }
            catch (ArgumentException e)
            {
                return StatusCode(204, null);
            }
        }

        [HttpPost("~/AddProductPhoto")]
        public async Task<ObjectResult> AddProductPhoto(string productTitle, string imageRef, bool isPriority)
        {
            try
            {
                var t = await _service.AddProductPhoto(productTitle, imageRef, isPriority);
                return StatusCode(200, t);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, null);
            }
        }

        [HttpDelete("~/DeleteProductPhoto")]
        public async Task<bool> DeleteProductPhoto(string productTitle, string imageRef)
        {
            return await _service.DeleteProductPhoto(productTitle, imageRef);
        }

        [HttpPost("~/UpdateProductPhoto")]
        public async Task<bool> UpdateProductPhoto(string productTitle, string oldImageRef, string newImageRef,
            bool isPriority)
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
        public async Task<bool> UpdateProjectPhoto(string projectTitle, string oldImageRef, string newImageRef,
            bool isPriority)
        {
            return await _service.UpdateProjectPhoto(projectTitle, oldImageRef, newImageRef, isPriority);
        }
    }
}