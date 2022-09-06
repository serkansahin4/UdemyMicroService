using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Udemy.IdentityServer.Models;

namespace Udemy.IdentityServer.Services
{
    public class IdentityResourcePasswordCheck : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourcePasswordCheck(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(context.UserName);
            if (user==null)
            {
                context.Result.CustomResponse.Add("errors", "Parola veya şifreniz hatalı.");
                return;
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(user, context.Password);
            if (passwordCheck==false)
            {
                context.Result.CustomResponse.Add("errors", "Parola veya şifreniz hatalı.");
                return;
            }
            context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
        }
    }
}
