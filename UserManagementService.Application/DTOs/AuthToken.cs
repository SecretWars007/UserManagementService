namespace UserManagementService.Application.DTOs
{
    public class AuthToken
    {
        public string Token { get; }

        public AuthToken(string token)
        {
            Token = token;
        }
    }
}
