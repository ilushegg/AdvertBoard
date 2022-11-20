using AdvertBoard.AppServices.ProductImage.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Domain.UserAvatar> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id);
        }

    }
}
