using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRolesService rolesService;
        private readonly ICryptoService cryptoService;
        public IConfiguration Configuration { get; }

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IRolesService rolesService,
        IConfiguration configuration, ICryptoService cryptoService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.rolesService = rolesService;
            this.cryptoService = cryptoService;
            Configuration = configuration;

        }

        public async Task<LoginResult> SignIn(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email) ?? throw new AuthException("Incorrect email or password", ErrorCodes.InvalidCredentials);

            if (!user.EmailConfirmed)
                throw new AuthException("Account has not been activated", ErrorCodes.AccountNotConfirmed);

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                return new LoginResult(token, user);
            }

            throw new AuthException("Incorrect email or password", ErrorCodes.InvalidCredentials);
        }

        public async Task<SignUpResult> SignUp(string email, string password, string userName)
        {
            var user = User.Create(email, userName);
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded && rolesService.AdmitRole((await rolesService.GetRoleId(Enums.RoleName.User)), user))
            {

                return new SignUpResult(user);
            }

            return null;
        }

        public async Task<bool> ResetPassword(string userId, string newPassword, string token)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            token = cryptoService.Decrypt(token);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }

        public async Task<string> GeneratedConfirmEmailUrl(string userId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Cannot find user", ErrorCodes.EntityNotFound);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = cryptoService.Encrypt(token);
            return $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}account/confirm?userId={user.Id}&token={token}";

        }

        public async Task<string> GenerateResetPasswordUrl(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email) ?? await this.userManager.FindByEmailAsync(Constants.FakeEmail);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            token = cryptoService.Encrypt(token);
            return $"{Configuration.GetValue<string>(AppSettingsKeys.ClientAddress)}account/confirm/password?userId={user.Id}&token={token}";
        }

        public async Task<bool> ConfirmAccount(string userId, string token)
        {
            token = cryptoService.Decrypt(token);
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            var result = await userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<bool> ConfirmResetPassword(string userId, string token)
        {
            token = cryptoService.Decrypt(token);
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            var result = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            return result;
        }

        public async Task<bool> EmailExists(string email)
            => await this.userManager.FindByEmailAsync(email) != null;

        public async Task<bool> UserNameExists(string userName)
            => await this.userManager.FindByNameAsync(userName) != null;

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await this.userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Constants:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}