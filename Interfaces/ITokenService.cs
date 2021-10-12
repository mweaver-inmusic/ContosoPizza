using Microsoft.AspNetCore.Identity;

namespace ContosoPizza.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user);
    }
}