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
                var res = new Views.Response.Error
                {
                    Type = ex.Source,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                return BadRequest(res);
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
                var res = new Views.Response.Error
                {
                    Type = ex.Source,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                return BadRequest(res);
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
                var res = new Views.Response.Error
                {
                    Type = ex.Source,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
                return BadRequest(res);
            }
        }

        [HttpPut("{id}")]
        public void Update(int id, [FromBody] string value)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
