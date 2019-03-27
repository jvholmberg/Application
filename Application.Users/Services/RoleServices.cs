using System.Linq;

namespace Application.Users.Services
{

    public interface IRoleService
    {
        Entities.Role FindById(int id);
        Entities.Role FindByName(string name);
        Entities.Role FindByCode(string code);
    }

    public class RoleService : IRoleService
    {

        private readonly UsersContext _UsersContext;

        public RoleService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
        }

        public Entities.Role FindById(int id)
        {
            try
            {
                var role = _UsersContext
                    .Roles
                    .Find(id);
                return role;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Role FindByName(string name)
        {
            try
            {
                var role = _UsersContext
                    .Roles
                    .FirstOrDefault(rle => rle.Name.Equals(name));
                return role;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Role FindByCode(string code)
        {
            try
            {
                var role = _UsersContext
                    .Roles
                    .FirstOrDefault(rle => rle.Code.Equals(code));
                return role;
            }
            catch
            {
                return null;
            }
        }
    }
}
