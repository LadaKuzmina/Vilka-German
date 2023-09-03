using Entity;

namespace ServerInitializer.Scripts;

public interface IHtmlParseDb
{
    Product GetProduct();
    List<string> GetHeadingFilters();
}