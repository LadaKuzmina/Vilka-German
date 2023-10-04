using Entity;

namespace BackServer.Repositories;

public interface IFilterVisitor
{
    Task<IEnumerable<Entity.Property>> GetAllHeadingOneFilters(string headingOneTitle);
    Task<IEnumerable<Entity.Property>> GetAllHeadingTwoFilters(string headingTwoTitle);
}