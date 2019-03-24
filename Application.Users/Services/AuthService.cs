using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Users.Entities;

namespace Application.Users.Services
{
    public interface IAuthService
    {
        Task<Entities.User> Register(Views.RegisterRequest req);
        Task<Entities.User> Validate();
        Task<Entities.User> Refresh();
    }

    public class AuthService : IAuthService
    {


        public AuthService()
        {
        }

        public Task<User> Register(Views.RegisterRequest req)
        {
            throw new NotImplementedException();
        }

        public Task<User> Validate()
        {
            throw new NotImplementedException();
        }

        public Task<User> Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
