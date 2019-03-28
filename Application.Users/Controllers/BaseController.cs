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
    public class BaseController : ControllerBase
    {
        private readonly Services.IBaseService _BaseService;

        public BaseController(Services.IBaseService baseService)
        {
            _BaseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _BaseService.GetAll();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _BaseService.GetById(id);
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
        public async Task<IActionResult> Register([FromBody] Views.Request.Register req)
        {
            try
            {
                var res = await _BaseService.Register(req);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Views.Request.Update req)
        {
            try
            {
                var res = await _BaseService.Update(id, req);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                var res = await _BaseService.Deactivate(id);
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
