using HtmlAgilityPack;
using NpgsqlDbExtensions.Enums;

namespace ServerInitializer.Services;

public interface IDbFillingService
{
    Task<IEnumerable<string>> InsertProductCategory(string categoryRef, string headingOneTitle, string headingTwoTitle,
        string? headingThreeProperty);

    Task InsertProduct(string productRef, string headingOneTitle, string headingTwoTitle,
        string? headingThreeProperty);

    Task InsertFilters(string categoryRef, string headingTitle, Headings headings);
}