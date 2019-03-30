using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            catch (Exception ex)
            {
                var err = new Views.Response.Error
                {
                    Type = ex.Source,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
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
                var err = new Views.Response.Error
                {
                    Type = ex.Source,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                return BadRequest(err);
            }
        }
    }
}
