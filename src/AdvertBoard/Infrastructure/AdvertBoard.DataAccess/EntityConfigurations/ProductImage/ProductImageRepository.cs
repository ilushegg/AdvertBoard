using AdvertBoard.AppServices.ProductImage.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.DataAccess.EntityConfigurations.ProductImage
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly IRepository<Domain.ProductImage> _repository;

        public ProductImageRepository(IRepository<Domain.ProductImage> repository)
        {
            _repository = repository;
        }


        public async Task<IReadOnlyCollection<ProductImageDto>> GetAllByProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(p => p.ProductId == productId).
                Select(p => new ProductImageDto
                {
                    FilePath = p.Image.FilePath,
                    ProductId = p.ProductId

                }).ToListAsync();
        }

        public async Task AddAsync(Domain.ProductImage productImage, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(productImage);
        }

        public async Task EditAsync(Domain.ProductImage productImage, CancellationToken cancellationToken)
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

        public async Task<Domain.ProductImage> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id);
        }

    }
}
