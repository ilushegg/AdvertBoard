using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.Contracts
{
    public class UserDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Эл. адрес.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Аватар.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        public string UserRole { get; set; }
    }
}
