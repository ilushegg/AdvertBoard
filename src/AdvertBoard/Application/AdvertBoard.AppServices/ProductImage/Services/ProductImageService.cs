using AdvertBoard.AppServices.ProductImage.Repositories;
using AdvertBoard.Infrastructure.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.AppServices.ProductImage.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string uploads;


        public ProductImageService(IProductImageRepository productImageRepository, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _productImageRepository = productImageRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        }

        public async Task AddAsync(Guid productId, IFormFile file, CancellationToken cancellationToken)
        {
            var productImage = new Domain.ProductImage()
            {
                ProductId = productId
            };
            if (file != null)
            {
                var uniqueFileName = _fileService.GetUniqueFileName(file.FileName);
                var filePath = Path.Combine(uploads, uniqueFileName);

                var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Dispose();
                productImage.FilePath = filePath;
            }
            await _productImageRepository.AddAsync(productImage, cancellationToken);
        }

        public async Task EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            var productImage = await _productImageRepository.GetById(id, cancellationToken);
            if (productImage == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором {id} не найдено.");
            }
            if (file != null)
            {
                var uniqueFileName = _fileService.GetUniqueFileName(file.FileName);
                var filePath = Path.Combine(uploads, uniqueFileName);

                var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Dispose();

                productImage.FilePath = filePath;
            }
            await _productImageRepository.EditAsync(productImage, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var productImage = await _productImageRepository.GetById(id, cancellationToken);
            if (productImage == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором {id} не найдено.");
            }
            if (productImage.FilePath != null)
            {
                var filePath = Path.Combine(uploads, productImage.FilePath);
                File.Delete(filePath);
            }
            await _productImageRepository.Delete(id, cancellationToken);
        }

    }
}
