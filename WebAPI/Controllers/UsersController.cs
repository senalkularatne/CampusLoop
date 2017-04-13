using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UCEvents_WebAPI.Infrastructure;
using UCEvents_WebAPI.Models;

namespace UCEvents_WebAPI.Controllers
{
    /// <summary>
    /// User Management
    /// </summary>
    [MobileAppController]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// Returns all the users
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.ModelFactory.Create(u)));
        }

        /// <summary>
        /// Get the user by UserID
        /// </summary>
        /// <param name="Id" >UserID</param>
        /// <returns></returns>
        [Route("api/user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(await this.ModelFactory.Create(user));
            }

            return NotFound();
        }

        /// <summary>
        /// Get the user by username
        /// </summary>
        /// <param name="username">Valid username for user</param>
        /// <returns></returns>
        [Route("api/user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(await this.ModelFactory.Create(user));
            }

            return NotFound();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="createUserModel">New User Information</param>
        /// <returns></returns>
        [Route("api/user/create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get Employee Data from GP using the EmployeeId and inserting it into Employee Table
            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, await ModelFactory.Create(user));
        }

        /// <summary>
        /// Change the user password
        /// </summary>
        /// <param name="changePasswordModel">Old and new password information</param>
        /// <returns></returns>
        [Route("api/user/changepassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), changePasswordModel.OldPassword, changePasswordModel.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        /// <summary>
        /// Deletes the user
        /// </summary>
        /// <param name="id">UserID for the user</param>
        /// <returns></returns>
        [Route("api/user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Assigns the specified roles to the user, and removes the rest
        /// </summary>
        /// <param name="id">UserID for the User</param>
        /// <param name="rolesToAssign">Array of Roles for the user</param>
        /// <returns></returns>
        [Route("user/{id:guid}/roles")]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, string[] rolesToAssign)
        {
            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {
                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}