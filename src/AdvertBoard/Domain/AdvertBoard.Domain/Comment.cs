using AdvertBoard.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class Comment
    {

        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public CommentStatus Status { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public Advertisement Advertisement { get; set; }

        public Guid AdvertisementId { get; set; }

    }
}
