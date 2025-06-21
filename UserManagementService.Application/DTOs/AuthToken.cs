using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
