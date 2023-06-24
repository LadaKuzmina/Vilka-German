using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BackServer.Contexts;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BackServer.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoVisitor _visitor;
        private readonly IPhotoChanger _changer;

        public PhotoService(IPhotoVisitor visitor, IPhotoChanger changer)
        {
            _visitor = visitor;
            _changer = changer;
        }

        public async Task<string?> GetPrimaryProductPhoto(string productTitle)
        {
            if (productTitle == "")
                throw new ArgumentException("Название продукта не может быть пустой строкой");

            var primaryPhoto = await _visitor.GetPrimaryProductPhoto(productTitle);
            if (primaryPhoto == null)
                throw new ArgumentException("Такого продукта не существует или у него нет приоритетных фото");

            return primaryPhoto;
        }

        public async Task<IEnumerable<string>> GetAllProductPhoto(string productTitle)
        {
            if (productTitle == "")
                throw new ArgumentException("Название продукта не может быть пустой строкой");

            var allPhoto = await _visitor.GetAllProductPhoto(productTitle);
            if (!allPhoto.Any())
                throw new ArgumentException("Такого продукта не существует или у него нет фотографий");

            return allPhoto;
        }

        public async Task<string?> GetPrimaryProjectPhoto(string projectTitle)
        {
            if (projectTitle == "")
                throw new ArgumentException("Название проекта не может быть пустой строкой");

            var primaryPhoto = await _visitor.GetPrimaryProjectPhoto(projectTitle);
            if (primaryPhoto == null)
                throw new ArgumentException("Такого проекта не существует или у него нет приоритетных фото");

            return primaryPhoto;
        }

        public async Task<IEnumerable<string>> GetAllProjectPhoto(string projectTitle)
        {
            if (projectTitle == "")
                throw new ArgumentException("Название проекта не может быть пустой строкой");

            var allPhoto = await _visitor.GetAllProjectPhoto(projectTitle);
            if (!allPhoto.Any())
                throw new ArgumentException("Такого проекта не существует или у него нет фотографий");

            return allPhoto;
        }

        public async Task<bool> AddProductPhoto(string productTitle, string imageRef, bool isPriority)
        {
            return await _changer.AddProductPhoto(productTitle, imageRef, isPriority);
        }

        public async Task<bool> DeleteProductPhoto(string productTitle, string imageRef)
        {
            return await _changer.DeleteProductPhoto(productTitle, imageRef);
        }

        public async Task<bool> UpdateProductPhoto(string productTitle, string oldImageRef, string newImageRef,
            bool isPriority)
        {
            return await _changer.UpdateProductPhoto(productTitle, oldImageRef, newImageRef, isPriority);
        }

        public async Task<bool> AddProjectPhoto(string projectTitle, string imageRef, bool isPriority)
        {
            return await _changer.AddProjectPhoto(projectTitle, imageRef, isPriority);
        }

        public async Task<bool> DeleteProjectPhoto(string projectTitle, string imageRef)
        {
            return await _changer.DeleteProjectPhoto(projectTitle, imageRef);
        }

        public async Task<bool> UpdateProjectPhoto(string projectTitle, string oldImageRef, string newImageRef,
            bool isPriority)
        {
            return await _changer.UpdateProjectPhoto(projectTitle, oldImageRef, newImageRef, isPriority);
        }
    }
}