using System.Linq;
using System.Threading.Tasks;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Interfaces;
using DbEntity;
using Microsoft.EntityFrameworkCore;

namespace BackServer.RepositoryChangers.Implementations
{
    public class PhotoChangerDb : IPhotoChanger
    {
        private readonly TestContext _context;

        public PhotoChangerDb(TestContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProductPhoto(string productTitle, string imageRef, bool isPriority)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Title == productTitle);
            if (product == null)
                return false;

            var productImage =
                await _context.ProductImages.FirstOrDefaultAsync(x =>
                    x.product_id == product.Id && x.ImageRef == imageRef);
            if (productImage != null)
                return true;

            productImage = new ProductImages() {Product = product, ImageRef = imageRef, IsPriority = isPriority};
            await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductPhoto(string productTitle, string imageRef)
        {
            var productImage =
                await _context.ProductImages.FirstOrDefaultAsync(x =>
                    x.Product.Title == productTitle && x.ImageRef == imageRef);
            if (productImage == null)
                return true;

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductPhoto(string productTitle, string oldImageRef, string newImageRef,
            bool isPriority)
        {
            var productImage =
                await _context.ProductImages.FirstOrDefaultAsync(x =>
                    x.Product.Title == productTitle && x.ImageRef == oldImageRef);
            if (productImage == null)
                return false;

            productImage.ImageRef = newImageRef;
            productImage.IsPriority = isPriority;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddProjectPhoto(string projectTitle, string imageRef, bool isPriority)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Title == projectTitle);
            if (project == null)
                return false;

            var projectImage =
                await _context.ProjectImages.FirstOrDefaultAsync(x =>
                    x.project_id == project.Id && x.ImageRef == imageRef);
            if (projectImage != null)
                return true;

            projectImage = new ProjectImages() {Project = project, ImageRef = imageRef, IsPriority = isPriority};
            await _context.ProjectImages.AddAsync(projectImage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectPhoto(string projectTitle, string imageRef)
        {
            var projectImage =
                await _context.ProjectImages.FirstOrDefaultAsync(x =>
                    x.Project.Title == projectTitle && x.ImageRef == imageRef);
            if (projectImage == null)
                return true;

            _context.ProjectImages.Remove(projectImage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProjectPhoto(string projectTitle,  string oldImageRef, string newImageRef, bool isPriority)
        {
            var projectImage =
                await _context.ProjectImages.FirstOrDefaultAsync(x =>
                    x.Project.Title == projectTitle && x.ImageRef == oldImageRef);
            if (projectImage == null)
                return false;

            projectImage.ImageRef = newImageRef;
            projectImage.IsPriority = isPriority;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}