using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Posts.Controllers
{
    [Route("api/posts/{postId}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly Services.ICommentService _CommentService;
        private readonly Core.Util.IJwt _Jwt;

        public CommentsController(Services.ICommentService commentService)
        {
            _CommentService = commentService;
            _Jwt = new Core.Util.Jwt();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        // POST: api/posts/{id}/comments (Add comment)
        [HttpPost]
        public async Task<IActionResult> Create([FromHeader]string authorization, [FromBody]Views.Request.CreateComment req)
        {
            try
            {
                var userId = (int)_Jwt.GetUserId(authorization);
                return Ok();
            }
            catch (Exception ex)
            {
                var err = new Core.Views.Response.Error(ex);
                return BadRequest(err);
            }
        }

        // PUT: api/posts/{id}/comments/{id} (Edit comment)

        // DELETE: api/posts/{id}/comments/{id} (Delete comment)





        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
