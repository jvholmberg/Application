using System.Linq;

namespace Application.Users.Services
{

    public interface ILanguageService
    {
        Entities.Language FindById(int id);
        Entities.Language FindByName(string name);
        Entities.Language FindByCode(string code);
    }

    public class LanguageService : ILanguageService
    {

        private readonly UsersContext _UsersContext;

        public LanguageService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
        }


    }
}
