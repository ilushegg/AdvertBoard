using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Contracts
{
    public class RegisterUserDto
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }
    }
}
