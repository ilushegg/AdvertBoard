using AdvertBoard.AppServices.AdvertisementImage.Repositories;
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
    public class AdvertisementImageService : IAdvertisementImageService
    {
        private readonly IAdvertisementImageRepository _productImageRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string uploads;


        public AdvertisementImageService(IAdvertisementImageRepository productImageRepository, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _productImageRepository = productImageRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        }

        public async Task AddAsync(Guid productId, Guid[] files, CancellationToken cancellationToken)
        {
            foreach (var file in files)
            {
                var productImage = new Domain.AdvertisementImage()
                {
                    AdvertisementId = productId
                };
                if (file != null)
                {
                    productImage.ImageId = file;
                }
                await _productImageRepository.AddAsync(productImage, cancellationToken);
            }
        }

        public void Add(Guid productId, Guid[] files)
        {
            foreach (var file in files)
            {
                var productImage = new Domain.AdvertisementImage()
                {
                    AdvertisementId = productId
                };
                if (file != null)
                {
                    productImage.ImageId = file;
                }
                _productImageRepository.Add(productImage);
            }
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

/*                productImage.FilePath = filePath;*/
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
/*            if (productImage.FilePath != null)
            {
                var filePath = Path.Combine(uploads, productImage.FilePath);
                File.Delete(filePath);
            }*/
            await _productImageRepository.Delete(id, cancellationToken);
        }

    }
}
