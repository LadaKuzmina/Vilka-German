using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BackServer.Config;
using Entity;
using HtmlAgilityPack;

namespace BackServer.Scripts;

public class DbAutoInsert
{
    private static string siteUrl = "https://vk196.ru";
    private static string catalogUrl = "https://vk196.ru/catalog";
    private readonly DownloadConfigurations _downloadConfigurations;

    public DbAutoInsert(DownloadConfigurations downloadConfigurations)
    {
        _downloadConfigurations = downloadConfigurations;
    }

    public async Task<Product> GetProduct(string productTitle)
    {
        return await GetProduct(new Uri($"{catalogUrl}/{productTitle}"));
    }

    public async Task<Product> GetProduct(Uri uri)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = uri,
            Method = HttpMethod.Get
        };

        var response = await new HttpClient().SendAsync(request);
        var product = new Product();
        if (response.IsSuccessStatusCode)
        {
            var htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(await response.Content.ReadAsStringAsync());

            product.Title =
                htmlSnippet.DocumentNode.SelectSingleNode("//h1[@class='card-name']").InnerText.Replace("&quot;", "\"").Split("\n")[0];

            var productCost = htmlSnippet.DocumentNode.SelectSingleNode("//div[@class='card-cost-value']").InnerText
                .Trim().Replace("&nbsp;", "").Replace(".", ",").Replace("₽", "руб.").Replace("м2", "м\u00b2")
                .Split(" ");
            product.Price = float.Parse(productCost[0]);
            product.UnitMeasurement = productCost[1];
            product.Available = true;

            foreach (var imageNode in htmlSnippet.DocumentNode.SelectNodes(
                         "//div[@class='card-photos-nav-item']//img"))
            {
                var imageRef = imageNode.Attributes["data-src"].Value;
                var imageName = imageRef.Split("/").Last();

                await DownloadImage(siteUrl + imageRef.Replace("preview", "large"), imageName);
                product.ImageRefs.Add(imageName);
            }

            foreach (var propertyNode in htmlSnippet.DocumentNode.SelectNodes(
                         "//div[@class='id-param-list']//ul[@class='shop_param__list']//li"))
            {
                var text = propertyNode.InnerText.Split(":");
                var property = new Property(text[0], new[] {text[1]});
                product.Properties.Add(property);
            }
        }

        product.Description = "";
        return product;
    }

    private async Task DownloadImage(string imageRef, string imageName)
    {
        using WebClient client = new WebClient();
        client.DownloadFileAsync(new Uri(imageRef), _downloadConfigurations.ImageDirectory + imageName);
    }

    public async Task<List<string>> GetRefs(string catalogTitle)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri =
                new Uri($"{catalogUrl}/{catalogTitle}"),
            Method = HttpMethod.Get
        };


        var response = await new HttpClient().SendAsync(request);

        var refs = new List<string>(30);
        if (response.IsSuccessStatusCode)
        {
            var htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(await response.Content.ReadAsStringAsync());

            refs.AddRange(htmlSnippet.DocumentNode.SelectNodes("//a[@class='shop-photo']")
                .Select(productParam => productParam.Attributes["href"].Value));
        }

        return refs;
    }
}