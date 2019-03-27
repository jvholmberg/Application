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

        public Entities.Language FindById(int id)
        {
            try
            {
                var language = _UsersContext
                    .Languages
                    .Find(id);
                return language;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Language FindByName(string name)
        {
            try
            {
                var language = _UsersContext
                    .Languages
                    .FirstOrDefault(lng => lng.Name.Equals(name));
                return language;
            }
            catch
            {
                return null;
            }
        }

        public Entities.Language FindByCode(string code)
        {
            try
            {
                var language = _UsersContext
                    .Languages
                    .FirstOrDefault(lng => lng.Code.Equals(code));
                return language;
            }
            catch
            {
                return null;
            }
        }
    }
}
