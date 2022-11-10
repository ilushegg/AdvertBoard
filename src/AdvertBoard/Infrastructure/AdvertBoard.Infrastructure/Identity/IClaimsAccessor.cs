using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Infrastructure.Identity;

public interface IClaimsAccessor
{
    Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellationToken);

}
