using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Views.Response;

namespace Application.Users.Services
{

    public interface IStatusService
    {
        Entities.Status FindByName(string name);
    }

    public class StatusService : IStatusService
    {

        private readonly UsersContext _UsersContext;

        public StatusService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
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
    }
}
