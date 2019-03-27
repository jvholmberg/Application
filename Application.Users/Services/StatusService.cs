using System.Linq;

namespace Application.Users.Services
{

    public interface IStatusService
    {
        Entities.Status FindById(int id);
        Entities.Status FindByName(string name);
        Entities.Status FindByCode(string code);
    }

    public class StatusService : IStatusService
    {

        private readonly UsersContext _UsersContext;

        public StatusService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
        }

        public Entities.Status FindById(int id)
        {
            try
            {
                var status = _UsersContext
                    .Statuses
                    .Find(id);
                return status;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Status FindByName(string name)
        {
            try
            {
                var status = _UsersContext
                    .Statuses
                    .FirstOrDefault(sts => sts.Name.Equals(name));
                return status;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Status FindByCode(string code)
        {
            try
            {
                var status = _UsersContext
                    .Statuses
                    .FirstOrDefault(sts => sts.Code.Equals(code));
                return status;
            }
            catch
            {
                return null;
            }
        }
    }
}
