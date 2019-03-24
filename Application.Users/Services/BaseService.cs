using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Users.Entities;

namespace Application.Users.Services
{
    public interface IBaseService
    {
        Task<Entities.User> GetById(int id);
        Task<IEnumerable<Entities.User>> Update();
        Task<IEnumerable<Entities.User>> GetAll();

    }

    public class BaseService : IBaseService
    {


        public BaseService()
        {
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Update()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
