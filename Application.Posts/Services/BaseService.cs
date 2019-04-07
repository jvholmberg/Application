using System;
using System.Linq;

namespace Application.Posts.Services
{
    public interface IBaseService
    {
        Entities.Status FindStatusById(int id);
        Entities.Status FindStatusByName(string name);
        Entities.Type FindTypeById(int id);
        Entities.Type FindTypeByName(string name);
    }

    public class BaseService : IBaseService
    {

        private readonly PostsContext _PostsContext;

        public BaseService(PostsContext postsContext)
        {
            _PostsContext = postsContext;
        }

        public Entities.Status FindStatusById(int id)
        {
            try
            {
                var status = _PostsContext
                    .Statuses
                    .Find(id);
                return status;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Status FindStatusByName(string name)
        {
            try
            {
                var status = _PostsContext
                    .Statuses
                    .FirstOrDefault(sts => sts.Name.Equals(name));
                return status;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Type FindTypeById(int id)
        {
            try
            {
                var type = _PostsContext
                    .Types
                    .Find(id);
                return type;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Type FindTypeByName(string name)
        {
            try
            {
                var type = _PostsContext
                    .Types
                    .FirstOrDefault(tpe => tpe.Name.Equals(name));
                return type;
            }
            catch
            {
                return null;
            }
        }

    }
}
