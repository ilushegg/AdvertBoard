using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class UserAvatar : FileUpload
    {
        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
