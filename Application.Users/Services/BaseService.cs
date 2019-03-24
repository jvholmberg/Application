using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services
{
    public interface IBaseService
    {
        Task<Views.Response.User> Register(Views.Request.Register req);
    }

    public class BaseService : IBaseService
    {

        private readonly UsersContext _UsersContext;

        public BaseService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
        }

        public async Task<Views.Response.User> Register(Views.Request.Register req)
        {
            try
            {
                // Check if data was provided
                if (string.IsNullOrWhiteSpace(req.Email)
                    || string.IsNullOrWhiteSpace(req.Password)
                    || string.IsNullOrWhiteSpace(req.PasswordVerify))
                {
                    throw new Exception();
                }

                // Check if email already in use
                var user = await _UsersContext
                    .Users
                    .SingleOrDefaultAsync(usr => usr.Email.Equals(req.Email));
                if (user != null)
                {
                    throw new Exception();
                }

                // Check if passwords match
                if (!req.Password.Equals(req.PasswordVerify))
                {
                    throw new Exception();
                }

                // Create new user
                user = new Entities.User
                {
                    Email = req.Email,
                    Password = req.Password,
                };

                // Persist user
                await _UsersContext.SaveChangesAsync();

                return new Views.Response.User
                {
                    Email = req.Email,
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
