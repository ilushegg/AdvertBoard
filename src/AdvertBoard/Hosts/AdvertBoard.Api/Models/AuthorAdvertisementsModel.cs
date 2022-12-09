﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Contracts
{
    public class AuthorAdvertisementsModel
    {
        /// <summary>
        /// Смещение.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Лимит.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Идентификатор автора.
        /// </summary>
        public Guid AuthorId { get; set; }

        
    }
}