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
        private readonly Core.Util.IJwt _Jwt;

        public AuthController(Services.IAuthService authService)
        {
            _AuthService = authService;
            _Jwt = new Core.Util.Jwt();
        }

        [HttpGet]
        public IActionResult Validate()
        {
            var res = new Core.Views.Confirmation("ok");
            return Ok(res);
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

        [HttpGet("{refreshToken}")]
        public async Task<IActionResult> Refresh([FromHeader]string authorization, string refreshToken)
        {
            try
            {
                var userId = (int)_Jwt.GetUserId(authorization);
                var accessToken = _Jwt.getAccessToken(authorization);
                var res = await _AuthService.Refresh(userId, accessToken, refreshToken);
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

        [HttpDelete]
        public async Task<IActionResult> Destroy([FromHeader]string authorization)
        {
            try
            {
                var userId = (int)_Jwt.GetUserId(authorization);
                await _AuthService.Destroy(userId);
                var res = new Core.Views.Confirmation("destroyed");
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
