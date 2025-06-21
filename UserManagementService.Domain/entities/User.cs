using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementService.Domain.entities
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public Guid RoleId { get; private set; }
        public Role? Role { get; private set; } // navegación

        public Profile? Profile { get; private set; } // navegación

        public User(string email, string passwordHash, Guid roleId)
        {
            Email = email;
            PasswordHash = passwordHash;
            RoleId = roleId;
        }

        private User() { } // requerido por EF Core
    }
}
