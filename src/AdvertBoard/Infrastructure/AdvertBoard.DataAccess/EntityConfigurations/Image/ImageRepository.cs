
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.DataAccess.EntityConfigurations.Image
{
    public class ImageRepository : IImageRepository
    {
        private readonly IRepository<Domain.Image> _repository;

        public ImageRepository(IRepository<Domain.Image> repository)
        {
            _repository = repository;
        }



        public async Task AddAsync(Domain.Image image, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(image);
        }

        public async Task EditAsync(Domain.Image image, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(image);
        }

        public async Task Delete(Domain.Image image, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(image);
        }

        public async Task<Domain.Image> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id);
        }

    }
}
