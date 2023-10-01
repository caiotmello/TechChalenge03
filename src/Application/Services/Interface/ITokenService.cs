using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Services.Interface
{
    public interface ITokenService
    {
        public Task<string> GenerateAsync(IdentityUser user);
        public Task<IList<Claim>> GenerateClaimsAsync(IdentityUser user);
    }
}
