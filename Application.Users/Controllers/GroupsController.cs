using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Users.Controllers
{
    [Route("api/users/groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {

        private readonly Services.IGroupService _GroupService;

        public GroupsController(Services.IGroupService groupService)
        {
            _GroupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _GroupService.GetAll();
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
                var res = await _GroupService.GetById(id);
                return Ok(res);
            }
            catch (Core.Exceptions.NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Views.Request.CreateGroup req)
        {
            try
            {
                var res = await _GroupService.Create(req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Views.Request.UpdateGroup req)
        {
            try
            {
                var res = await _GroupService.Update(id, req);
                return Ok(res);
            }
            catch (Core.Exceptions.NotFoundException)
            {
                return NotFound();
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
                var res = await _GroupService.Delete(id);
                return Ok(res);
            }
            catch (Core.Exceptions.NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }
    }
}
