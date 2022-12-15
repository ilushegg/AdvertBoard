using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.Domain;
using System.Linq.Expressions;
using AdvertBoard.AppServices.User.Repositories;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <inheritdoc />
public class UserRoleRepository : IUserRoleRepository
{
    private readonly IRepository<Domain.UserRole> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public UserRoleRepository(IRepository<Domain.UserRole> repository)
    {
        _repository = repository;
    }


    public async Task<Domain.UserRole> FindWhereAsync(Expression<Func<Domain.UserRole, bool>> predicate, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAll().Where(predicate).FirstOrDefaultAsync();

        return data;
    }

    public Domain.UserRole FindWhere(Expression<Func<Domain.UserRole, bool>> predicate)
    {
        var data = _repository.GetAll().Where(predicate).FirstOrDefault();

        return data;
    }

    public async Task AddAsync(Domain.UserRole role)
    {
        await _repository.AddAsync(role);
    }

    public Guid Add(Domain.UserRole role)
    {
        _repository.Add(role);
        return role.UserId;
    }

    public async Task<Domain.UserRole> FindByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id);
    }

    public Guid Edit(Domain.UserRole role)
    {
        _repository.Update(role);
        return role.UserId;
    }

    public async Task EditAsync(Domain.UserRole role, CancellationToken cancellation)
    {
        await _repository.UpdateAsync(role);
    }
}