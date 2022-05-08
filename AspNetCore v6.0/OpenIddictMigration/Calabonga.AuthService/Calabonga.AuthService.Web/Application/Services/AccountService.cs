﻿using AutoMapper;
using Calabonga.AuthService.Domain.Base;
using Calabonga.AuthService.Infrastructure;
using Calabonga.AuthService.Web.Definitions.Identity;
using Calabonga.AuthService.Web.Endpoints.ProfileEndpoints.ViewModels;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.Microservices.Core.Extensions;
using Calabonga.Microservices.Core.Validators;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Calabonga.AuthService.Web.Application.Services;

/// <summary>
/// Account service
/// </summary>
public class AccountService : IAccountService
{
    private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
    private readonly ILogger<AccountService> _logger;
    private readonly ApplicationClaimsPrincipalFactory _claimsFactory;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AccountService(
        IUserStore<ApplicationUser> userStore,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<RoleManager<ApplicationRole>> loggerRole,
        IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
        IUnitOfWork<ApplicationDbContext> unitOfWork,
        ILogger<AccountService> logger,
        ILogger<UserManager<ApplicationUser>> loggerUser,
        ApplicationClaimsPrincipalFactory claimsFactory,
        IHttpContextAccessor httpContext,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _claimsFactory = claimsFactory;
        _httpContext = httpContext;
        _mapper = mapper;

        // We need to created a custom instance for current service
        // It'll help to use Transaction in the Unit Of Work
        _userManager = new UserManager<ApplicationUser>(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, loggerUser);
        var roleStore = new RoleStore<ApplicationRole, ApplicationDbContext, Guid>(_unitOfWork.DbContext);
        _roleManager = new RoleManager<ApplicationRole>(roleStore, roleValidators, keyNormalizer, errors, loggerRole);
    }

    /// <inheritdoc />
    public Guid GetCurrentUserId()
    {
        var identity = _httpContext.HttpContext?.User.Identity;
        var identitySub = identity?.GetSubjectId();
        return identitySub?.ToGuid() ?? Guid.Empty;
    }

    /// <summary>
    /// Returns <see cref="ApplicationUser"/> instance after successful registration
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<OperationResult<UserProfileViewModel>> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<UserProfileViewModel>();
        var user = _mapper.Map<ApplicationUser>(model);
        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        var result = await _userManager.CreateAsync(user, model.Password);
        const string role = AppData.ManagerRoleName;

        if (result.Succeeded)
        {
            if (await _roleManager.FindByNameAsync(role) == null)
            {
                operation.Exception = new MicroserviceUserNotFoundException();
                operation.AddError(AppData.Exceptions.UserNotFoundException);
                return await Task.FromResult(operation);
            }
            await _userManager.AddToRoleAsync(user, role);
            await AddClaimsToUser(_userManager, user, role);
            var profile = _mapper.Map<ApplicationUserProfile>(model);
            var profileRepository = _unitOfWork.GetRepository<ApplicationUserProfile>();

            await profileRepository.InsertAsync(profile, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                var principal = await _claimsFactory.CreateAsync(user);
                operation.Result = _mapper.Map<UserProfileViewModel>(principal.Identity);
                operation.AddSuccess(AppData.Messages.UserSuccessfullyRegistered);
                _logger.LogInformation(operation.GetMetadataMessages());
                await transaction.CommitAsync(cancellationToken);
                _logger.MicroserviceUserRegistration(model.Email);
                return await Task.FromResult(operation);
            }
        }
        var errors = result.Errors.Select(x => $"{x.Code}: {x.Description}");
        operation.AddError(string.Join(", ", errors));
        operation.Exception = _unitOfWork.LastSaveChangesResult.Exception;
        await transaction.RollbackAsync(cancellationToken);
        _logger.MicroserviceUserRegistration(model.Email, operation.Exception);
        return await Task.FromResult(operation);
    }

    /// <summary>
    /// Returns user profile
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public async Task<OperationResult<UserProfileViewModel>> GetProfileByIdAsync(string identifier)
    {
        var operation = OperationResult.CreateResult<UserProfileViewModel>();
        var claimsPrincipal = await GetUserClaimsByIdAsync(identifier);
        operation.Result = _mapper.Map<UserProfileViewModel>(claimsPrincipal.Identity);
        return await Task.FromResult(operation);
    }

    /// <summary>
    /// Returns user profile
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<OperationResult<UserProfileViewModel>> GetProfileByEmailAsync(string email)
    {
        var operation = OperationResult.CreateResult<UserProfileViewModel>();
        var claimsPrincipal = await GetUserClaimsByEmailAsync(email);
        operation.Result = _mapper.Map<UserProfileViewModel>(claimsPrincipal.Identity);
        return await Task.FromResult(operation);
    }

    /// <summary>
    /// Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public async Task<ClaimsPrincipal> GetUserClaimsByIdAsync(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            throw new MicroserviceException();
        }
        var userManager = _userManager;
        var user = await userManager.FindByIdAsync(identifier);
        if (user == null)
        {
            throw new MicroserviceUserNotFoundException();
        }

        var defaultClaims = await _claimsFactory.CreateAsync(user);
        return defaultClaims;
    }

    /// <summary>
    /// Returns ClaimPrincipal by user identity
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<ClaimsPrincipal> GetUserClaimsByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new MicroserviceException();
        }
        var userManager = _userManager;
        var user = await userManager.FindByEmailAsync(email);
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
        var userManager = _userManager;
        return userManager.FindByIdAsync(id.ToString());
    }

    /// <summary>
    /// Returns current user account information or null when user does not logged in
    /// </summary>
    /// <returns></returns>
    public async Task<ApplicationUser> GetCurrentUserAsync()
    {
        var userManager = _userManager;
        var userId = GetCurrentUserId().ToString();
        var user = await userManager.FindByIdAsync(userId);
        return user;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ApplicationUser>> GetUsersByEmailsAsync(IEnumerable<string> emails)
    {
        var userManager = _userManager;
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
        var userManager = _userManager;
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
        var userManager = _userManager;
        return await userManager.GetUsersInRoleAsync(roleName);
    }

    #region privates

    private async Task AddClaimsToUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
    {
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.FirstName));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
    }

    #endregion
}