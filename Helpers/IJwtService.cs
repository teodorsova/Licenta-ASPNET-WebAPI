using System.IdentityModel.Tokens.Jwt;
using auth.Models;

namespace auth.Helpers
{
    public interface IJwtService
    {
        public string Generate(UserModel user);
        public JwtSecurityToken Verify(string jwt);
    }
}