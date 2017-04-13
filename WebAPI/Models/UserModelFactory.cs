using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using UCEvents_WebAPI.Infrastructure;

namespace UCEvents_WebAPI.Models
{
    public class UserModelFactory
    {
        private UrlHelper _urlHelper;
        private ApplicationUserManager _appUserManager;

        /// <summary>
        /// Helper class to create User and Roles related return models
        /// </summary>
        /// <param name="request"></param>
        /// <param name="appUserManager"></param>
        public UserModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _appUserManager = appUserManager;
        }

        /// <summary>
        /// Creates a UserReturnModel asynchronously
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public async Task<UserReturnModel> Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Roles = await _appUserManager.GetRolesAsync(appUser.Id),
                Claims = await _appUserManager.GetClaimsAsync(appUser.Id)
            };
        }

        /// <summary>
        /// Creates the Return Model for Auth Roles
        /// </summary>
        /// <param name="appRole">Role to return</param>
        /// <returns>A return model for Roles</returns>
        public RoleReturnModel Create(IdentityRole appRole)
        {
            return new RoleReturnModel
            {
                Url = _urlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };
        }
    }
}