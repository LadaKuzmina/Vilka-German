using BackServer.Controllers;
using DbEntity;
using HtmlAgilityPack;
using Npgsql;
using NpgsqlDbExtensions.Enums;
using ServerInitializer.Scripts;
using Product = Entity.Product;

namespace ServerInitializer.Services;

public class DbFillingService : IDbFillingService
{
    private DbDataRequests _dbDataRequests;
    private readonly ProductsHandlersController _productsController;
    private readonly HeadingsHandlersController _headingsController;
    private readonly PropertiesHandlersController _propertiesController;
    private readonly ImageHandlersController _imageController;
    private readonly UnitMeasurementController _unitMeasurementController;
    private readonly ILogger<DbFillingService> _logger;

    public DbFillingService(DbDataRequests dbDataRequests, ProductsHandlersController productsController,
        HeadingsHandlersController headingsController,
        PropertiesHandlersController propertiesController,
        ImageHandlersController imageController,
        UnitMeasurementController unitMeasurementController, ILogger<DbFillingService> logger)
    {
        _dbDataRequests = dbDataRequests;
        _productsController = productsController;
        _headingsController = headingsController;
        _propertiesController = propertiesController;
        _imageController = imageController;
        _unitMeasurementController = unitMeasurementController;
        _logger = logger;
    }


    public async Task<IEnumerable<string>> InsertProductCategory(string categoryRef, string headingOneTitle,
        string headingTwoTitle, string? headingThreeProperty)
    {
        var refs = await GetProductsRefs(categoryRef);
        var exceptionProducts = new List<string>();
        foreach (var productRef in refs)
            await InsertProduct(productRef, headingOneTitle, headingTwoTitle, headingThreeProperty);

        await InsertFilters(categoryRef, headingTwoTitle, Headings.HeadingTwo);
        return exceptionProducts;
    }

    public async Task InsertFilters(string categoryRef, string headingTitle, Headings headings)
    {
        var html = await _dbDataRequests.GetCategoryHtml(new Uri(categoryRef));
        if (html==null)
            _logger.Log(LogLevel.Warning, $"Не удалось полусить страницу {categoryRef}");

        var parseHtml = new ParseHtml(html);
        var filters = parseHtml.GetHeadingFilters();
        switch (headings)
        {
            case Headings.HeadingOne:
                foreach (var filter in filters)
                {
                    await _propertiesController.AddFilterHeadingOne(filter, headingTitle);
                }
                break;
            case Headings.HeadingTwo:
                foreach (var filter in filters)
                {
                    await _propertiesController.AddFilterHeadingTwo(filter, headingTitle);
                }
                break;
        }
    }

    public async Task InsertProduct(string productRef, string headingOneTitle, string headingTwoTitle,
        string? headingThreeProperty)
    {
        var htmlDocument = await _dbDataRequests.GetProductHtml(new Uri(productRef));
        if (htmlDocument == null)
        {
            _logger.Log(LogLevel.Warning, $"Не удалось получить продукт по ссылке {productRef}");
            return;
        }

        Entity.Product product;
        try
        {
            product = await ConvertHtmlToProduct(htmlDocument);
        }
        catch (ArgumentException e)
        {
            _logger.Log(LogLevel.Warning, e.Message);
            return;
        }

        product.HeadingOne = headingOneTitle;
        product.HeadingTwo = headingTwoTitle;
        product.ProductFamilyTitle = product.Title;

        try
        {
            await InsertProduct(product, headingThreeProperty);
        }
        catch (ArgumentException e)
        {
            _logger.Log(LogLevel.Warning, e.Message);
            return;
        }


        await DownloadImages(product.ImageRefs);
        product.ImageRefs = product.ImageRefs.Select(x => x.Split("/").Last()).ToList();
        await InsertProductImages(product);
        await InsertProductProperties(product);
    }

    private async Task InsertProduct(Entity.Product product, string? headingThreePropertyTitle)
    {
        if (headingThreePropertyTitle != null)
            await TryAddHeadingThree(product, headingThreePropertyTitle);

        await _unitMeasurementController.AddUnitMeasurement(product.UnitMeasurement);

        var statusCode = await _productsController.AddProduct(product);
        if (statusCode.StatusCode != 200)
            throw new ArgumentException($"Не удалось добавить продукт {product.Title}");
    }

    private async Task TryAddHeadingThree(Product product, string? headingThreePropertyTitle)
    {
        var headingThreeProperty = product.Properties
            .FirstOrDefault(x => x.Title == headingThreePropertyTitle);

        if (headingThreeProperty != null)
        {
            await _propertiesController.AddPropertyValue(headingThreeProperty.Title, headingThreeProperty.Values);

            var headingThreeValue = headingThreeProperty.Values.First();
            var headingThree = new Entity.HeadingThree(headingThreeValue, product.HeadingTwo, null, null);
            var codeResult = await _headingsController.AddHeadingThree(headingThree);

            if (codeResult.StatusCode == 200)
                product.HeadingThree = headingThreeValue;
            else
                _logger.Log(LogLevel.Warning,
                    $"Не удалось добавить заголовок третьего уровня {headingThree.Title} к продукту {product.Title}");
        }
    }

    private async Task InsertProductProperties(Product product)
    {
        for (var i = 0; i < Math.Min(3, product.Properties.Count); i++)
        {
            product.Properties[i].IsPriority = true;
        }

        foreach (var property in product.Properties)
        {
            var successes =
                await _propertiesController.AddProductProperties(product.Title, product.Properties.ToArray());
            foreach (var suc in successes)
            {
                if (!suc)
                    _logger.Log(LogLevel.Warning,
                        $"Не удалось добавить свойство {property.Title} со значением {property.Values} к продукту {product.Title}");
            }
        }
    }

    private async Task InsertProductImages(Entity.Product product)
    {
        var priority = true;
        foreach (var imageRef in product.ImageRefs)
        {
            var objectResult = await _imageController.AddProductPhoto(product.Title, imageRef, priority);
            if (objectResult.StatusCode != 200)
                _logger.Log(LogLevel.Warning, $"Не удалось добавить изображение {imageRef} к продукту {product.Title}");
            else
                priority = false;
        }
    }

    private async Task DownloadImages(IEnumerable<string> imageRefs)
    {
        foreach (var imageRef in imageRefs)
        {
            var imageUri = $"https://vk196.ru{imageRef.Replace("preview", "large")}";
            var imageName = imageRef.Split("/").Last();
            await _dbDataRequests.DownloadImage(imageUri, imageName);
        }
    }

    private async Task<Entity.Product> ConvertHtmlToProduct(HtmlDocument htmlDocument)
    {
        var htmlParse = new ParseHtml(htmlDocument);
        var product = htmlParse.GetProduct();
        if (product.Title == null)
            throw new ArgumentException("Не удалось получить продукт");

        product.ImageRefs = htmlParse.GetProductImageRefs();
        product.Properties = htmlParse.GetProductProperties();

        return product;
    }

    private async Task<List<string>> GetProductsRefs(string categoryName)
    {
        var refs = new List<string>();
        var pageNum = 1;
        while (true)
        {
            var newRefs = await _dbDataRequests.GetProductsRefs($"{categoryName}/page{pageNum}");
            if (newRefs.Count == 0)
                break;
            refs.AddRange(newRefs);
            pageNum++;
        }

        return refs;
    }
}