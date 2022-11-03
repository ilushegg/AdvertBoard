using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    public class ProductImage : FileUpload
    {
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
    }
}
