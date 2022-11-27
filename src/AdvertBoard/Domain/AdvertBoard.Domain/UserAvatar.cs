using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class UserAvatar
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public Image Image { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

    }
}
