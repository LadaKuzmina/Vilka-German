using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackServer.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<string?> GetPrimaryProductPhoto(string productTitle);
        Task<IEnumerable<string>> GetAllProductPhoto(string productTitle);
        Task<string?> GetPrimaryProjectPhoto(string projectTitle);
        Task<IEnumerable<string>> GetAllProjectPhoto(string projectTitle);
        
        
        Task<bool> AddProductPhoto(string productTitle, string imageRef, bool isPriority);
        Task<bool> DeleteProductPhoto(string productTitle, string imageRef);
        Task<bool> UpdateProductPhoto(string productTitle, string oldImageRef, string newImageRef, bool isPriority);
        Task<bool> AddProjectPhoto(string projectTitle, string imageRef, bool isPriority);
        Task<bool> DeleteProjectPhoto(string projectTitle, string imageRef);
        Task<bool> UpdateProjectPhoto(string projectTitle, string oldImageRef, string newImageRef, bool isPriority);
    }
}