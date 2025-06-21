using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementService.Domain.entities
{
    public class Profile
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public string? Address { get; private set; }

        public Profile(Guid userId, string fullName, string? address = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("El ID del usuario es obligatorio.", nameof(userId));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("El nombre completo es obligatorio.", nameof(fullName));

            UserId = userId;
            FullName = fullName;
            Address = address;
        }

        private Profile() { }
    }
}
