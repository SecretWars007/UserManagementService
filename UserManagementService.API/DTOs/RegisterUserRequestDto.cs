using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementService.API.DTOs
{
    public class RegisterUserRequestDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Guid RoleId { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
    }
}
