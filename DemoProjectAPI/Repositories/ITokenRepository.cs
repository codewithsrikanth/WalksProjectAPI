using Microsoft.AspNetCore.Identity;

namespace DemoProjectAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
