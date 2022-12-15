using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class UserRole
    {
        public User User {get; set;}
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
