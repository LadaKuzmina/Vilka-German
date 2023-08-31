using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BackServer.Config;
using Entity;
using HtmlAgilityPack;

namespace ServerInitializer.Scripts;

public class DbAutoInsert
{
    private static string siteUrl = "https://vk196.ru";
    private static string catalogUrl = "https://vk196.ru/catalog";
    private readonly DownloadConfigurations _downloadConfigurations;

    public DbAutoInsert(DownloadConfigurations downloadConfigurations)
    {
        _downloadConfigurations = downloadConfigurations;
    }

    public async Task<HtmlDocument?> GetHtml(Uri uri)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = uri,
            Method = HttpMethod.Get
        };

        var response = await new HttpClient().SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await response.Content.ReadAsStringAsync());

            return htmlDocument;
        }

        return null;
    }

    public async Task DownloadImage(string imageRef, string imageName)
    {
        using var client = new WebClient();
        client.DownloadFileAsync(new Uri(imageRef), _downloadConfigurations.ImageDirectory + imageName);
    }

    public async Task<List<string>> GetProductsRefs(string catalogTitle)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri =
                new Uri($"{catalogUrl}/{catalogTitle}"),
            Method = HttpMethod.Get
        };


        var response = await new HttpClient().SendAsync(request);

        var refs = new List<string>();
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