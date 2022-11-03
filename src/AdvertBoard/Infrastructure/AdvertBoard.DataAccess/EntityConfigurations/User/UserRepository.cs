using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.Domain;
using System.Linq.Expressions;

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


    public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        var data = _repository.GetAllFiltered(predicate);

        return await data.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
    
    public Task Add(User user)
    {
        return _repository.AddAsync(user);
    }

    public async Task<User> FindById(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id);
    }
}