using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class ProductImage
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public Image Image { get; set; }
        
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
