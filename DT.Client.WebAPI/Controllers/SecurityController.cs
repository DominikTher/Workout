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

        public SecurityController(IAuthenticationService authenticationService) => this.authenticationService = authenticationService;

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
    }
}