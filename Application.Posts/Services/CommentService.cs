using System;
using System.Threading.Tasks;

namespace Application.Posts.Services
{
    public interface ICommentService
    {
        Task<Views.Response.Post> Create(int postId, int userId, Views.Request.CreateComment req);
        // Task<bool> Destroy(int userId);
    }

    public class CommentService : ICommentService
    {

        private readonly PostsContext _PostsContext;
        private readonly IBaseService _BaseService;

        public CommentService(PostsContext postsContext)
        {
            _PostsContext = postsContext;
            _BaseService = new BaseService(postsContext);
        }

        public async Task<Views.Response.Post> Create(int postId, int userId, Views.Request.CreateComment req)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
