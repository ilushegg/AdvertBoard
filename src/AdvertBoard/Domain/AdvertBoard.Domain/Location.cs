using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Domain
{
    /// <summary>
    /// Место объявления.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Страна.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        public string? Street { get; set; }

        /// <summary>
        /// Номер.
        /// </summary>
        public string? Number { get; set; }

        /// <summary>
        /// Объявление.
        /// </summary>
        public Advertisement Advertisement { get; set; }


    }
}
