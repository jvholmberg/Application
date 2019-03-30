using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services
{
    public interface IBaseService
    {
        Entities.Status FindStatusById(int id);
        Entities.Status FindStatusByName(string name);
        Entities.Role FindRoleById(int id);
        Entities.Role FindRoleByName(string name);
        Entities.Language FindLanguageById(int id);
        Entities.Language FindLanguageByName(string name);
        Entities.Language FindLanguageByCode(string code);
    }

    public class BaseService : IBaseService
    {

        private readonly UsersContext _UsersContext;

        public BaseService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
        }

        public Entities.Status FindStatusById(int id)
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

        public Entities.Status FindStatusByName(string name)
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

        public Entities.Role FindRoleById(int id)
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

        public Entities.Role FindRoleByName(string name)
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

        public Entities.Language FindLanguageById(int id)
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

        public Entities.Language FindLanguageByName(string name)
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

        public Entities.Language FindLanguageByCode(string code)
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
