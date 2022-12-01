
using AdvertBoard.Infrastructure.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertBoard.DataAccess.EntityConfigurations.Image;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Image.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string uploads;


        public ImageService(IImageRepository imageRepository, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _imageRepository = imageRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        }

        public async Task<Guid> AddAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var image = new Domain.Image();

            if (file != null)
            {
                var uniqueFileName = _fileService.GetUniqueFileName(file.FileName);
                var filePath = Path.Combine(uploads, uniqueFileName);

                var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Dispose();
                image.FilePath = filePath;
            }

            await _imageRepository.AddAsync(image, cancellationToken);
            return image.Id;

        }

        public async Task<Guid> EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.GetById(id, cancellationToken);
            if (image == null)
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

                image.FilePath = filePath;
            }
            await _imageRepository.EditAsync(image, cancellationToken);
            return id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.GetById(id, cancellationToken);
            if (image == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором {id} не найдено.");
            }
            /*            if (productImage.FilePath != null)
                        {
                            var filePath = Path.Combine(uploads, productImage.FilePath);
                            File.Delete(filePath);
                        }*/
            await _imageRepository.Delete(image, cancellationToken);
        }

    }
}
