using DT.Business.Interface.Authentication;
using DT.Client.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DT.Client.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SecurityController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public SecurityController(IAuthenticationService authenticationService)
            => this.authenticationService = authenticationService;

        [HttpPost("register")]
        public async Task<ActionResult> Register(AppUser appUser)
        {
            var newUser = await authenticationService.RegisterAsnyc(appUser);

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
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            var auth = await authenticationService.ValidateExternalUser(new AppUser { Email = payload.Email, Name = payload.Name });

            return Ok(auth);
        }

        [HttpPost("login-refresh")]
        public ActionResult LoginRefresh(AppUserAuth appUserAuth)
        {
            var auth = authenticationService.Refresh(appUserAuth.Token, appUserAuth.RefreshToken);

            return Ok(auth);
        }
    }
}