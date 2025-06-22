namespace UserManagementService.Application.DTOs
{
    public class RegisterUserRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Guid RoleId { get; set; }
        public string FullName { get; set; } = default!;
        public string? Address { get; set; }
    }
}
