using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Posts.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        // POST: api/posts (Create post)

        // PUT: api/posts (Edit post)

        // GET: api/posts (Get all posts)

        // GET: api/posts/{id} (Get post)

        // DELETE: api/posts/{id} (Delete post)




        // POST: api/posts/{id}/comments (Add comment)

        // PUT: api/posts/{id}/comments/{id} (Edit comment)

        // DELETE: api/posts/{id}/comments/{id} (Delete comment)




        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
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
