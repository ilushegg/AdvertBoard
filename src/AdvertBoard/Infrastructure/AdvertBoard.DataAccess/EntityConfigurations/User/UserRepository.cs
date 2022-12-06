using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.Domain;
using System.Linq.Expressions;
using AdvertBoard.AppServices.User.Repositories;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private readonly IRepository<Domain.User> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public UserRepository(IRepository<Domain.User> repository)
    {
        _repository = repository;
    }


    public async Task<Domain.User> FindWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
    {
        var data = _repository.GetAllFiltered(predicate);

        return await data.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
    
    public Task Add(Domain.User user)
    {
        return _repository.AddAsync(user);
    }

    public async Task<Domain.User> FindById(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id);
    }

    public void Edit(Domain.User user)
    {
        _repository.Update(user);
    }

    public async Task EditAsync(Domain.User user, CancellationToken cancellation)
    {
       await _repository.UpdateAsync(user);
    }
}