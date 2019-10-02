using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.EntityFrameworkCore.UnitOfWork;
using Calabonga.EntityFrameworkCore.UnitOfWork.Framework.Extensions;
using Calabonga.Microservice.IdentityModule.Core;
using Calabonga.Microservice.IdentityModule.Core.Exceptions;
using Calabonga.Microservice.IdentityModule.Data;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Auth;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Settings;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.Validations.Base;
using Calabonga.Microservice.IdentityModule.Web.Infrastructure.ViewModels.AccountViewModels;
using Calabonga.OperationResultsCore;
using IdentityModel;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Services
{
    /// <summary>
    /// Account service
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork<ApplicationUser, ApplicationRole> _unitOfWork;
        private readonly ILogger<AccountService> _logger;
        private readonly ApplicationClaimsPrincipalFactory _claimsFactory;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly CurrentAppSettings _appSettings;

        /// <inheritdoc />
        public AccountService(
            IUnitOfWork<ApplicationUser, ApplicationRole> unitOfWork,
            ILogger<AccountService> logger,
            IOptions<CurrentAppSettings> options,
            ApplicationClaimsPrincipalFactory claimsFactory,
            IHttpContextAccessor httpContext,
            IMapper mapper)
        {
            _appSettings = options.Value;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _claimsFactory = claimsFactory;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public Guid GetCurrentUserId()
        {
            var identity = _httpContext.HttpContext?.User.Identity;
            var identitySub = identity.GetSubjectId();
            return identitySub.ToGuid();
        }

        /// <summary>
        /// Returns <see cref="ApplicationUser"/> instance after successful registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUserProfileViewModel>> RegisterAsync(RegisterViewModel model)
        {
            var operation = OperationResult.CreateResult<ApplicationUserProfileViewModel>();
            var user = _mapper.Map<ApplicationUser>(model);
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                var userManager = _unitOfWork.GetUserManager();
                var result = await userManager.CreateAsync(user, model.Password);
                var role = AppData.ManagerRoleName;

                if (result.Succeeded)
                {
                    var roleManager = _unitOfWork.GetRoleManager();
                    if (await roleManager.FindByNameAsync(role) == null)
                    {
                        operation.Exception = new MicroserviceUserNotFoundException();
                        operation.AddError(AppData.Exceptions.UserNotFoundException);
                        return await Task.FromResult(operation);
                    }
                    await userManager.AddToRoleAsync(user, role);
                    await AddClaimsToUser(userManager, user, role);
                    var profile = _mapper.Map<ApplicationUserProfile>(model);
                    var profileRepository = _unitOfWork.GetRepository<ApplicationUserProfile>();
                    profile.ApplicationUserId = user.Id;
                    await profileRepository.InsertAsync(profile);
                    await _unitOfWork.SaveChangesAsync();
                    if (_unitOfWork.LastSaveChangesResult.IsOk)
                    {
                        var principal = await _claimsFactory.CreateAsync(user);
                        operation.Result = _mapper.Map<ApplicationUserProfileViewModel>(principal.Identity);
                        operation.AddSuccess(AppData.Messages.UserSuccessfullyRegistered);
                        _logger.LogInformation(operation.GetMetadataMessages());
                        transaction.Commit();
                        return await Task.FromResult(operation);
                    }
                }
                var errors = result.Errors.Select(x => $"{x.Code}: {x.Description}");
                operation.AddError(string.Join(", ", errors));
                operation.Exception = _unitOfWork.LastSaveChangesResult.Exception;
                transaction.Rollback();
                return await Task.FromResult(operation);
            }
        }

        /// <summary>
        /// Returns user profile
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<OperationResult<ApplicationUserProfileViewModel>> GetProfileAsync(string identifier)
        {
            var operation = OperationResult.CreateResult<ApplicationUserProfileViewModel>();
            var claimsPrincipal = await GetUserClaimsAsync(identifier);
            operation.Result = _mapper.Map<ApplicationUserProfileViewModel>(claimsPrincipal.Identity);
            return await Task.FromResult(operation);
        }

        /// <summary>
        /// Returns ClaimPrincipal by user identity
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> GetUserClaimsAsync(string identifier)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                throw new MicroserviceException();
            }
            var userManager = _unitOfWork.GetUserManager();
            var user = await userManager.FindByIdAsync(identifier);
            if (user == null)
            {
                throw new MicroserviceUserNotFoundException();
            }

            var defaultClaims = await _claimsFactory.CreateAsync(user);
            return defaultClaims;
        }

        /// <summary>
        /// Returns user by his identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            var userManager = _unitOfWork.GetUserManager();
            return userManager.FindByIdAsync(id.ToString());
        }

        /// <summary>
        /// Returns current user account information or null when user does not logged in
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var userManager = _unitOfWork.GetUserManager();
            var userId = GetCurrentUserId().ToString();
            var user = await userManager.FindByIdAsync(userId);
            return user;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ApplicationUser>> GetUsersByEmailsAsync(IEnumerable<string> emails)
        {
            var userManager = _unitOfWork.GetUserManager();
            var result = new List<ApplicationUser>();
            foreach (var email in emails)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user != null && !result.Contains(user))
                {
                    result.Add(user);
                }
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Check roles for current user
        /// </summary>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        public async Task<PermissionValidationResult> IsInRolesAsync(string[] roleNames)
        {
            var userManager = _unitOfWork.GetUserManager();
            var userId = GetCurrentUserId().ToString();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var resultUserNotFound = new PermissionValidationResult();
                resultUserNotFound.AddError(AppData.Exceptions.UnauthorizedException);
                return await Task.FromResult(resultUserNotFound);
            }
            foreach (var roleName in roleNames)
            {
                var ok = await userManager.IsInRoleAsync(user, roleName);
                if (ok)
                {
                    return new PermissionValidationResult();
                }
            }

            var result = new PermissionValidationResult();
            result.AddError(AppData.Exceptions.UnauthorizedException);
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName)
        {
            var userManager = _unitOfWork.GetUserManager();
            return await userManager.GetUsersInRoleAsync(roleName);
        }

        #region privates

        private async Task AddClaimsToUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
        {
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
            await userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.GivenName, user.FirstName));
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }

        #endregion
    }
}

