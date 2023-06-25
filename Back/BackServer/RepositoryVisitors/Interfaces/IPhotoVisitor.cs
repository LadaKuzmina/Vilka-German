using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace BackServer.Repositories
{
    public interface IPhotoVisitor
    {
        Task<string?> GetPrimaryProductPhoto(string productTitle);
        Task<IEnumerable<string>> GetAllProductPhoto(string productTitle);
        Task<string?> GetPrimaryProjectPhoto(string projectTitle);
        Task<IEnumerable<string>> GetAllProjectPhoto(string projectTitle);
    }
}