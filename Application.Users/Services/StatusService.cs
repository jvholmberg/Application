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


    }
}
