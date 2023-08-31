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

    public Product GetProduct()
    {
        var productPrice = _htmlDocument.DocumentNode
            .SelectSingleNode("//div[@class='card-cost-value']")
            .InnerText
            .Trim()
            .Replace("&nbsp;", "")
            .Replace(".", ",")
            .Replace("руб.", "₽")
            .Replace("м2", "м\u00b2")
            .Split(" ");
        
        var product = new Product
        {
            Title = _htmlDocument.DocumentNode
                .SelectSingleNode("//h1[@class='card-name']")
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
        foreach (var imageNode in _htmlDocument.DocumentNode.SelectNodes(
                     "//div[@class='card-photos-nav-item']//img"))
        {
            var imageRef = imageNode.Attributes["data-src"].Value;
            imageRefs.Add(imageRef);
        }

        return imageRefs;
    }
    
    public List<Property> GetProductProperties()
    {
        var properties = new List<Property>();
        foreach (var propertyNode in _htmlDocument.DocumentNode.SelectNodes(
                     "//div[@class='id-param-list']//ul[@class='shop_param__list']//li"))
        {
            var text = propertyNode.InnerText.Split(":");
            var property = new Property(text[0], new[] {text[1]});
            properties.Add(property);
        }

        return properties;
    }

    public List<string> GetHeadingTwoFilters()
    {
        var headingTwoFilters = new List<string>();
        foreach (var headingTwoFilterNode in _htmlDocument.DocumentNode.SelectNodes(
                     "//div[@class='links-list']//span"))
        {
            headingTwoFilters.Add(headingTwoFilterNode.InnerText.Remove(':'));
        }

        return headingTwoFilters;
    }
}

