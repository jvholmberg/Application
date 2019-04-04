using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Users.Controllers
{
    [Route("api/users/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly Services.IAuthService _AuthService;

        public AuthController(Services.IAuthService authService)
        {
            _AuthService = authService;
        }

        [HttpGet("{refreshToken}")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            try
            {
                // TODO: Get userId from headers
                var res = await _AuthService.Refresh(1, "", refreshToken);
                return Ok(res);
            }
            catch (Core.Exceptions.UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Validate([FromBody]Views.Request.ValidateAuth req)
        {
            try
            {
                var res = await _AuthService.Validate(req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }
    }
}
