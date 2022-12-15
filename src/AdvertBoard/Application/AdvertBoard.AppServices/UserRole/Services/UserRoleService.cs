using AdvertBoard.DataAccess.EntityConfigurations.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.AppServices.UserRole.Services
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }


        public async Task AddAsync(Guid userId, string role, CancellationToken cancellationToken)
        {

            var userRole = new Domain.UserRole
            {
                UserId = userId,
                Role = role
            };
            await _userRoleRepository.AddAsync(userRole);

        }

        public async Task EditAsync(Guid userId, string role, CancellationToken cancellationToken)
        {
            var exUserRole = await _userRoleRepository.FindByUserIdAsync(userId, cancellationToken);

            if (exUserRole != null)
            {
                var userRole = new Domain.UserRole
                {
                    UserId = userId,
                    Role = role
                };
                await _userRoleRepository.AddAsync(userRole);


            }
        }
    }
}
