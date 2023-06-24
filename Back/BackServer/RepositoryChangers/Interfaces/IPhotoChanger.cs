using System.Threading.Tasks;

namespace BackServer.RepositoryChangers.Interfaces
{
    public interface IPhotoChanger
    {
        Task<bool> AddProductPhoto(string productTitle, string imageRef, bool isPriority);
        Task<bool> DeleteProductPhoto(string productTitle, string imageRef);
        Task<bool> UpdateProductPhoto(string productTitle, string oldImageRef, string newImageRef, bool isPriority);
        Task<bool> AddProjectPhoto(string projectTitle, string imageRef, bool isPriority);
        Task<bool> DeleteProjectPhoto(string projectTitle, string imageRef);
        Task<bool> UpdateProjectPhoto(string projectTitle, string oldImageRef, string newImageRef, bool isPriority);
    }
}