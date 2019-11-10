using DT.Business.Interface.Authentication;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using AppUserBusiness = DT.Business.Entities.AppUser;

namespace DT.Client.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SecurityController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IAppUserDataService appUserDataService;

        public SecurityController(IAuthenticationService authenticationService, IAppUserDataService appUserDataService)
            => (this.authenticationService, this.appUserDataService) = (authenticationService, appUserDataService);

        [HttpPost("register")]
        public async Task<ActionResult> Register(AppUser appUser)
        {
            var newUser = await appUserDataService.AddAsync(appUser);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public ActionResult Login(AppUser appUser)
        {
            var auth = authenticationService.ValidateUser(appUser);

            if (auth.IsAuthenticated == true)
                return Ok(auth);
            else
                return NotFound("Invalid UserName or Password");
        }

        [HttpPost("login-google")]
        public async Task<ActionResult> LoginGoogle([FromBody] string token)
        {
            // TODO: 
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);

            throw new NotImplementedException();
        }

        [HttpPost("login-refresh")]
        public ActionResult LoginRefresh(AppUserAuth appUserAuth)
        {
            var auth = authenticationService.Refresh(appUserAuth.Token, appUserAuth.RefreshToken);

            return Ok(auth);
        }
    }
}