using DT.Business.Interface.Authentication;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            return (appUserEntity == null) ? new AppUserAuth() : BuildUserAuthObject(appUserEntity);
        }

        public AppUserAuth Refresh(string token, string refreshToken)
        {
            var email = GetEmailFromExpiredToken(token);
            var appUser = appUserDataService.GetAppUser(email);

            if (appUser.RefreshToken != refreshToken)
                return new AppUserAuth();

            return BuildUserAuthObject(appUser);
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
            BuildAndSaveRefreshToken(appUser, appUserAuth);

            return appUserAuth;
        }

        private void BuildAndSaveRefreshToken(AppUser appUser, AppUserAuth appUserAuth)
        {
            var refreshToken = BuildRefreshToken();

            appUserAuth.RefreshToken = refreshToken;
            appUser.RefreshToken = refreshToken;
            appUserDataService.UpdateRefreshToken(appUser.Id, refreshToken);
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

        private string GetEmailFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = false
                },
                out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return jwtSecurityToken.Subject;
        }

        public string BuildRefreshToken()
        {
            var randomNumber = new byte[128];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
