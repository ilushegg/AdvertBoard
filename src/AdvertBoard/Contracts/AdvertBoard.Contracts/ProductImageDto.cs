using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Contracts
{
    public class ProductImageDto
    {
        public Guid ImageId { get; set; }
        public Guid ProductId { get; set; }
        public string FilePath { get; set; }

    }
}
