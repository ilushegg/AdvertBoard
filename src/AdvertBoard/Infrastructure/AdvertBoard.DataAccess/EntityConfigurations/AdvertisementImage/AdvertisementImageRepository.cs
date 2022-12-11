using AdvertBoard.AppServices.AdvertisementImage.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.DataAccess.EntityConfigurations.AdvertisementImage
{
    public class AdvertisementImageRepository : IAdvertisementImageRepository
    {
        private readonly IRepository<Domain.AdvertisementImage> _repository;

        public AdvertisementImageRepository(IRepository<Domain.AdvertisementImage> repository)
        {
            _repository = repository;
        }


        public async Task<IReadOnlyCollection<ProductImageDto>> GetAllByProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(p => p.AdvertisementId == productId).
                Select(p => new ProductImageDto
                {
                    ImageId = p.ImageId,
                    FilePath = p.Image.FilePath,
                    ProductId = p.AdvertisementId

                }).ToListAsync();
        }

        public async Task AddAsync(Domain.AdvertisementImage productImage, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(productImage);
        }

        public void Add(Domain.AdvertisementImage productImage)
        {
            _repository.Add(productImage);
        }

        public async Task EditAsync(Domain.AdvertisementImage productImage, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(productImage);
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

        public async Task<Domain.AdvertisementImage> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ICollection<Domain.AdvertisementImage>> GetAllByAdvertisementEntities(Guid productId, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(p => p.AdvertisementId == productId).ToListAsync();
        }
    }
}
