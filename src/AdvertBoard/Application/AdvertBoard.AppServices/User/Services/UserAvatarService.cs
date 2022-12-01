using AdvertBoard.AppServices.AdvertisementImage.Repositories;
using AdvertBoard.Infrastructure.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;

namespace AdvertBoard.AppServices.ProductImage.Services
{
    public class UserAvatarService : IUserAvatarService
    {
        private readonly IUserAvatarRepository _userAvatarRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string uploads;


        public UserAvatarService(IUserAvatarRepository userAvatarRepository, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _userAvatarRepository = userAvatarRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
        }

        public async Task AddAsync(Guid userId, IFormFile file, CancellationToken cancellationToken)
        {
            var userAvatar = new Domain.UserAvatar()
            {
                UserId = userId
            };
            if (file != null)
            {
                var uniqueFileName = _fileService.GetUniqueFileName(file.FileName);
                var filePath = Path.Combine(uploads, uniqueFileName);

                var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Dispose();
/*                userAvatar.FilePath = filePath;*/
            }
            await _userAvatarRepository.AddAsync(userAvatar, cancellationToken);
        }

        public async Task EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            var userAvatar = await _userAvatarRepository.GetById(id, cancellationToken);
            if (userAvatar == null)
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

/*                userAvatar.FilePath = filePath;*/
            }
            await _userAvatarRepository.EditAsync(userAvatar, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var userAvatar = await _userAvatarRepository.GetById(id, cancellationToken);
            if (userAvatar == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором {id} не найдено.");
            }
/*            if (userAvatar.FilePath != null)
            {
                var filePath = Path.Combine(uploads, userAvatar.FilePath);
                File.Delete(filePath);
            }*/
            await _userAvatarRepository.Delete(id, cancellationToken);
        }

    }
}
