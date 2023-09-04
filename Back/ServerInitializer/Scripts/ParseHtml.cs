using Entity;
using HtmlAgilityPack;

namespace ServerInitializer.Scripts;

public class ParseHtml : IHtmlParseDb
{
    private readonly HtmlDocument _htmlDocument;

    public ParseHtml(HtmlDocument htmlDocument)
    {
        _htmlDocument = htmlDocument;
    }

    public Product? GetProduct()
    {
        var priceNode = _htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-cost-value']");
        string[] productPrice;
        if (priceNode != null)
        {
            productPrice = priceNode
                .InnerText
                .Trim()
                .Replace("&nbsp;", "")
                .Replace(".", ",")
                .Replace("руб.", "₽")
                .Replace("м2", "м\u00b2")
                .Split(" ");
        }
        else
        {
            productPrice = new[] {"0", "₽"};
        }

        var titleNode = _htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='card-name']");
        if (titleNode == null)
            return null;


        var product = new Product
        {
            Title = titleNode
                .InnerText
                .Replace("&quot;", "\"")
                .Split("\n")[0],
            Price = float.Parse(productPrice[0]),
            UnitMeasurement = productPrice[1],
            Available = true,
            Description = ""
        };

        return product;
    }

    public List<string> GetProductImageRefs()
    {
        var imageRefs = new List<string>();
        var imageNodes = _htmlDocument.DocumentNode.SelectNodes(
            "//div[@class='card-photos-nav-item']//img");

        if (imageNodes == null) return imageRefs;

        foreach (var imageNode in imageNodes)
        {
            var imageRef = imageNode.Attributes["data-src"].Value;
            imageRefs.Add(imageRef);
        }

        return imageRefs;
    }

    public List<Property> GetProductProperties()
    {
        var properties = new List<Property>();
        var propertyNodes = _htmlDocument.DocumentNode.SelectNodes(
            "//div[@class='id-param-list']//ul[@class='shop_param__list']//li");

        if (propertyNodes == null) return properties;

        foreach (var propertyNode in propertyNodes)
        {
            var text = propertyNode.InnerText.Split(":");
            var property = new Property(text[0], new[] {text[1]});
            properties.Add(property);
        }

        return properties;
    }

    public List<string> GetHeadingFilters()
    {
        var headingFilters = new List<string>();
        var filterNodes = _htmlDocument.DocumentNode
            .SelectNodes("//div[@class='filternsv_search_param_name-group']");

        if (filterNodes == null) return headingFilters;

        foreach (var headingFilterNode in filterNodes)
        {
            headingFilters.Add(headingFilterNode.InnerText);
        }

        return headingFilters;
    }
}