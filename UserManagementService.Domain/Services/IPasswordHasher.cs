using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementService.Domain.Services
{
    public interface IPasswordHasher
    {
        string Hash(string password);
    }
}
