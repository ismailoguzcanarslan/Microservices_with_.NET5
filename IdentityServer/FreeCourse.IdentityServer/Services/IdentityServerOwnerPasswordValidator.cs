using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityServerOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityServerOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var isExistedUser = await _userManager.FindByEmailAsync(context.UserName);

            if(isExistedUser == null)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors", new List<string>() { "Wrong password or username" });

                context.Result.CustomResponse = errors;

                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(isExistedUser, context.Password);

            if (!passwordCheck)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors", new List<string>() { "Wrong password or username" });

                context.Result.CustomResponse = errors;
            }

            context.Result = new GrantValidationResult(isExistedUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
