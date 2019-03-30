using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Users.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Services.IUserService _UserService;

        public UsersController(Services.IUserService userService)
        {
            _UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _UserService.GetAll();
                return Ok(res);
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _UserService.GetById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Views.Request.CreateUser req)
        {
            try
            {
                var res = await _UserService.Create(req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Views.Request.UpdateUser req)
        {
            try
            {
                var res = await _UserService.Update(id, req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _UserService.Delete(id);
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
