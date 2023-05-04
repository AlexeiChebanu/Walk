using Microsoft.AspNetCore.Identity;

namespace WebApi.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
