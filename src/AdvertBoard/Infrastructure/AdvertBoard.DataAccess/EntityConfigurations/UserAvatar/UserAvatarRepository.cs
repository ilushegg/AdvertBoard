using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.DataAccess.EntityConfigurations.UserAvatar
{
    public class UserAvatarRepository : IUserAvatarRepository
    {
        private readonly IRepository<Domain.UserAvatar> _repository;

        public UserAvatarRepository(IRepository<Domain.UserAvatar> repository)
        {
            _repository = repository;
        }


        public async Task AddAsync(Domain.UserAvatar userAvatar, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(userAvatar);
        }

        public async Task EditAsync(Domain.UserAvatar userAvatar, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(userAvatar);
        }

        public async Task Delete(Guid productId, CancellationToken cancellationToken)
        {
            var productImage = await _repository.GetByIdAsync(productId);

            if (productImage == null)
            {
                throw new InvalidOperationException($"Изображение с идентификатором продукта {productId} не найдено.");
            }

            await _repository.DeleteAsync(productImage);
        }

        public async Task<UserAvatarDto> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetAll().Where(u => u.UserId == userId).
                Select(a => new UserAvatarDto
                {
                    FilePath = a.Image.FilePath,
                    Id = a.Id,
                    UserId = a.UserId,
                    ImageId = a.ImageId

                }).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                return new UserAvatarDto
                {
                    FilePath = null
                };
            }
            
            
        }


    }
}
