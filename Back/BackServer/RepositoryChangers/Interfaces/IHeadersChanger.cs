using System.Threading.Tasks;

namespace BackServer.RepositoryChangers.Interfaces
{
    public interface IHeadersChanger
    {
        Task AddHeadingOne(Entity.HeadingOne headingOne);
        Task AddHeadingTwo(Entity.HeadingTwo headingTwo);
        Task DeleteHeadingOne(int id);
        Task DeleteHeadingTwo(int id);
    }
}