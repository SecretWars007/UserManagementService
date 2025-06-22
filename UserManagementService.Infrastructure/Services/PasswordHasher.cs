using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Domain.Services;

namespace UserManagementService.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA512.HashData(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
