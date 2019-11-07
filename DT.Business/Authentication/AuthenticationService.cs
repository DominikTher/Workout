using DT.Business.Interface.Authentication;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DT.Business.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings jwtSettings;
        private readonly IAppUserDataService appUserDataService;

        public AuthenticationService(IOptions<JwtSettings> options, IAppUserDataService appUserDataService)
        {
            jwtSettings = options.Value;
            this.appUserDataService = appUserDataService;
        }

        public AppUserAuth ValidateUser(AppUser appUser)
        {
            var appUserEntity = appUserDataService.GetAppUser(appUser.Email, appUser.Password);

            return (appUserEntity == null) ? new AppUserAuth() : BuildUserAuthObject(appUser);
        }

        private AppUserAuth BuildUserAuthObject(AppUser appUser)
        {
            var appUserAuth = new AppUserAuth
            {
                Name = appUser.Name,
                IsAuthenticated = true,
                Email = appUser.Email
            };

            appUserAuth.Token = BuildJwtToken(appUserAuth);

            return appUserAuth;
        }

        private string BuildJwtToken(AppUserAuth appUserAuth)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUserAuth.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(jwtSettings.MinutesToExpire),
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
