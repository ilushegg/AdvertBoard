using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Contracts
{
    public class CategoryDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Идентификатор родителя категории.
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        public List<CategoryDto> Children { get; set; }
    }
}
