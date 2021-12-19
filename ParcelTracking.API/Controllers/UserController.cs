using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParcelTracking.API.Services.Authentication;

namespace ParcelTracking.API.Controllers
{
    [Authorize]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var validUser = this.authenticationService.Authenticate(user.Name, user.Password);

            return validUser != null ? Ok(validUser) : BadRequest();
        }
    }
}
