using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        public string? FilePath { get; set; }

        public AdvertisementImage AdvertisementImage { get; set; }

        public UserAvatar UserAvatar { get; set; }



    }
}
