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


    public async Task<UserDto> FindWhereAsync(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAll().Where(predicate).Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            UserRole = u.UserRole.Role,
            CreateDate = u.CreateDate,
            Mobile = u.Mobile,
            ActivationCode = u.ActivationCode,
            RecoveryCode = u.RecoveryCode
   
        }).FirstOrDefaultAsync();

        return data;
    }

    public UserDto FindWhere(Expression<Func<Domain.User, bool>> predicate)
    {
        var data = _repository.GetAll().Where(predicate).Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            UserRole = u.UserRole.Role,
            CreateDate = u.CreateDate,
            Mobile = u.Mobile,
            ActivationCode = u.ActivationCode,
            RecoveryCode = u.RecoveryCode
        }).FirstOrDefault();

        return data;
    }

    public Domain.User FindWhereEntity(Expression<Func<Domain.User, bool>> predicate)
    {
        var data = _repository.GetAll().Where(predicate).FirstOrDefault();

        return data;
    }

    public async Task AddAsync(Domain.User user)
    {
        await _repository.AddAsync(user);
    }

    public Guid Add(Domain.User user)
    {
        _repository.Add(user);
        return user.Id;
    }

    public async Task<Domain.User> FindById(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id);
    }

    public Guid Edit(Domain.User user)
    {
        _repository.Update(user);
        return user.Id;
    }

    public async Task EditAsync(Domain.User user, CancellationToken cancellation)
    {
       await _repository.UpdateAsync(user);
    }

    public async Task DeleteAsync(Domain.User user, CancellationToken cancellation)
    {
        await _repository.DeleteAsync(user);
    }
}