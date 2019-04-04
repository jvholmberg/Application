using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services
{
    public interface IUserService
    {
        Task<Views.Response.User> Create(Views.Request.CreateUser req);
        Task<IEnumerable<Views.Response.User>> GetAll();
        Task<Views.Response.User> GetById(int id);
        Task<Views.Response.User> Update(int id, Views.Request.UpdateUser req);
        Task<Views.Response.User> Delete(int id);
    }

    public class UserService : IUserService
    {

        private readonly UsersContext _UsersContext;
        private readonly IBaseService _BaseService;

        public UserService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
            _BaseService = new BaseService(usersContext);
        }

        public async Task<Views.Response.User> Create(Views.Request.CreateUser req)
        {
            try
            {
                // Check if data was provided
                if (string.IsNullOrWhiteSpace(req.Email)
                    || string.IsNullOrWhiteSpace(req.Password)
                    || string.IsNullOrWhiteSpace(req.PasswordVerify))
                {
                    throw new Core
                        .Exceptions
                        .InvalidArgumentsException("Email, Password or Verification was empty");
                }

                // Check if email already in use
                var user = await _UsersContext
                    .Users
                    .SingleOrDefaultAsync(usr => usr.Email.Equals(req.Email));
                if (user != null)
                {
                    throw new Core
                        .Exceptions
                        .ExistingFoundException("Email is already in use");
                }

                // Check if passwords match
                if (!req.Password.Equals(req.PasswordVerify))
                {
                    throw new Core
                        .Exceptions
                        .InvalidArgumentsException("Passwords did not match");
                }

                // Find status by name
                var statusName = Entities.StatusName.Pending.ToString();
                var status = _BaseService.FindStatusByName(statusName);

                // Find role by name
                var roleName = Entities.RoleName.User.ToString();
                var role = _BaseService.FindRoleByName(roleName);


                // Create new user
                user = new Entities.User
                {
                    Email = req.Email,
                    Password = req.Password,
                    Status = status,
                    Role = role,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _UsersContext.Users.Add(user);

                // Persist context
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.User(user);
                return view;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Views.Response.User>> GetAll()
        {
            try
            {
                var users = await _UsersContext
                    .Users
                    .ToListAsync();
                var views = users.Select(usr => new Views.Response.User(usr));
                return views;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Views.Response.User> GetById(int id)
        {
            try
            {
                // Get entity from db
                var user = await _UsersContext
                    .Users
                    .Include(usr => usr.Status)
                    .Include(usr => usr.Role)
                    .Include(usr => usr.Language)
                    .Include(usr => usr.Memberships)
                        .ThenInclude(mem => mem.Group)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // No entity was found
                if (user == null)
                {
                    throw new Core
                        .Exceptions
                        .NotFoundException("User not found");
                }

                var view = new Views.Response.User(user);

                return view;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Views.Response.User> Update(int id, Views.Request.UpdateUser req)
        {
            try
            {
                // Get entity from db
                var user = await _UsersContext
                    .Users
                    .Include(usr => usr.Status)
                    .Include(usr => usr.Role)
                    .Include(usr => usr.Language)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // No entity was found
                if (user == null)
                {
                    throw new Core
                        .Exceptions
                        .NotFoundException("User not found");
                }

                // Update Status if Provided
                if (req.StatusName != null)
                {
                    var status = _BaseService.FindStatusByName(req.StatusName);
                    user.Status = status;
                }

                // Update Role if Provided
                if (req.RoleName != null)
                {
                    var role = _BaseService.FindRoleByName(req.RoleName);
                    user.Role = role;
                }

                // Update Language if Provided
                if (req.LanguageCode != null)
                {
                    var language = _BaseService.FindLanguageByCode(req.LanguageCode);
                    user.Language = language;
                }

                // Update Email if Provided
                if (req.Email != null)
                {
                    user.Email = req.Email;
                }

                // Update FirstName if provided
                if (req.FirstName != null)
                {
                    user.FirstName = req.FirstName;
                }

                // Update LastName if provided
                if (req.LastName != null)
                {
                    user.LastName = req.LastName;
                }

                // Update Avatar if provided
                if (req.Avatar != null)
                {
                    user.Avatar = req.Avatar;
                }

                // Set last time of update
                user.LastUpdated = DateTime.UtcNow;

                // Update user
                _UsersContext
                    .Users
                    .Update(user);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.User(user);
                return view;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Views.Response.User> Delete(int id)
        {
            try
            {
                // Get entity from db
                var user = await _UsersContext
                    .Users
                    .Include(usr => usr.Memberships)
                        .ThenInclude(mem => mem.Group)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // No entity was found
                if (user == null)
                {
                    throw new Core
                        .Exceptions
                        .NotFoundException("User not found");
                }

                // Find status by name
                var statusName = Entities.StatusName.Inactive.ToString();
                var status = _BaseService.FindStatusByName(statusName);

                // Set status on user and memberships to inactive
                user.Status = status;
                user.Memberships.Select(mem => mem.Status = status);

                // Update user
                _UsersContext.Users.Update(user);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.User(user);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}