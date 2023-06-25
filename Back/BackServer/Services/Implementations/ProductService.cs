using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;
using Entity;
using Npgsql;
using NpgsqlDbExtensions.Enums;

namespace BackServer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductVisitor _visitor;
        private readonly IProductChanger _changer;

        public ProductService(IProductVisitor visitor, IProductChanger changer)
        {
            _visitor = visitor;
            _changer = changer;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _visitor.GetAll();
        }

        public async Task<IEnumerable<Product>> GetAvailable()
        {
            return await _visitor.GetAvailable();
        }

        public async Task<Product> GetByTitle(string title)
        {
            return await _visitor.GetByTitle(title);
        }

        public async Task<IEnumerable<Product>> GetPageHeadingOne(string headingOneTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements)
        {
            if (!CheckCorrectInput(headingOneTitle, pageNumber, countElements))
                return Array.Empty<Product>();


            var products = await _visitor.GetPageHeadingOne(headingOneTitle, productOrder, reqProperties, pageNumber,
                countElements);

            return products;
        }

        public async Task<IEnumerable<Product>> GetPageHeadingTwo(string headingTwoTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements)
        {
            if (!CheckCorrectInput(headingTwoTitle, pageNumber, countElements))
                return Array.Empty<Product>();

            return await _visitor.GetPageHeadingTwo(headingTwoTitle, productOrder, reqProperties, pageNumber,
                countElements);
        }

        public async Task<IEnumerable<Product>> GetPageHeadingThree(string headingThreeTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements)
        {
            if (!CheckCorrectInput(headingThreeTitle, pageNumber, countElements))
                return Array.Empty<Product>();

            return await _visitor.GetPageHeadingThree(headingThreeTitle, productOrder, reqProperties, pageNumber,
                countElements);
        }

        public async Task<int> GetCountPagesHeadingOne(string headingOneTitle, ProductOrders productOrder, Dictionary<string, HashSet<string>> reqProperties,
            int countElements)
        {
            return await _visitor.GetCountPagesHeadingOne(headingOneTitle, productOrder, reqProperties, countElements);
        }

        public async Task<int> GetCountPagesHeadingTwo(string headingTwoTitle, ProductOrders productOrder, Dictionary<string, HashSet<string>> reqProperties,
            int countElements)
        {
            return await _visitor.GetCountPagesHeadingTwo(headingTwoTitle, productOrder, reqProperties, countElements);
        }

        public async Task<bool> UpdatePopularity(string productTitle, int newPopularity)
        {
            return await _changer.UpdatePopularity(productTitle, newPopularity);
        }

        public async Task<bool> Add(Product product)
        {
            return await _changer.Add(product);
        }

        public async Task<bool> Delete(HashSet<string> productTitles)
        {
            return await _changer.Delete(productTitles);
        }

        public async Task<bool> Update(string oldProductTitle, Product product)
        {
            return await _changer.Update(oldProductTitle, product);
        }

        public async Task<bool> DeleteHeadingOneProducts(string headingOneTitle)
        {
            return await _changer.DeleteHeadingOneProducts(headingOneTitle);
        }

        public async Task<bool> DeleteHeadingTwoProducts(string headingTwoTitle)
        {
            return await _changer.DeleteHeadingTwoProducts(headingTwoTitle);
        }

        public async Task<bool> DeleteHeadingThreeProducts(string headingThreeTitle)
        {
            return await _changer.DeleteHeadingThreeProducts(headingThreeTitle);
        }

        private bool CheckCorrectInput(string title, int pageNumber, int countElements)
        {
            return title != "" && pageNumber >= 1 && countElements >= 1;
        }
    }
}