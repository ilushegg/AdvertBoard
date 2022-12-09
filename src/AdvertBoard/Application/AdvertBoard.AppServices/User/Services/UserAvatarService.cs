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

namespace AdvertBoard.AppServices.User.Services
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

        public async Task<Guid> GetAvatarByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var userAvatar = await _userAvatarRepository.GetByUserIdAsync(userId, cancellationToken);

                return userAvatar.ImageId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Guid GetAvatarByUserId(Guid userId)
        {
            try
            {
                Contracts.UserAvatarDto? userAvatar = _userAvatarRepository.GetByUserId(userId);
                if (userAvatar == null)
                {
                    return Guid.Empty;
                }
                else
                {
                return userAvatar.ImageId;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Guid> AddAsync(Guid userId, Guid imageId, CancellationToken cancellationToken)
        {
            var userAvatar = new Domain.UserAvatar()
            {
                UserId = userId
            };
            if (imageId != null)
            {
                userAvatar.ImageId = imageId;
            }
            await _userAvatarRepository.AddAsync(userAvatar, cancellationToken);

            return userAvatar.Id;
        }

        public async Task<Guid> EditAsync(Guid id, Guid imageId, CancellationToken cancellationToken)
        {
            var userAvatar = _userAvatarRepository.GetByUserId(id);
            if (userAvatar == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором {id} не найдено.");
            }
            if (imageId != null)
            {

                await _userAvatarRepository.EditAsync(new Domain.UserAvatar
                {
                    Id = userAvatar.Id,
                    UserId = userAvatar.UserId,
                    ImageId = imageId
                }, cancellationToken);

                
            }
            return userAvatar.Id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            /*var userAvatar = await _userAvatarRepository.GetById(id, cancellationToken);
            if (userAvatar == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором {id} не найдено.");
            }
*//*            if (userAvatar.FilePath != null)
            {
                var filePath = Path.Combine(uploads, userAvatar.FilePath);
                File.Delete(filePath);
            }*//*
            await _userAvatarRepository.Delete(id, cancellationToken);*/
        }

    }
}
