using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services
{
    public interface IBaseService
    {
        Task<Views.Response.User> Register(Views.Request.Register req);
        Task<IEnumerable<Views.Response.User>> GetAll();
        Task<Views.Response.User> GetById(int id);
        Task<Views.Response.User> Update(int id, Views.Request.Update req);
        Task<Views.Response.User> Deactivate(int id);
    }

    public class BaseService : IBaseService
    {

        private readonly UsersContext _UsersContext;

        private readonly IStatusService _StatusService;
        private readonly IRoleService _RoleService;
        private readonly ILanguageService _LanguageService;

        public BaseService(UsersContext usersContext)
        {
            _UsersContext = usersContext;

            _StatusService = new StatusService(usersContext);
            _RoleService = new RoleService(usersContext);
            _LanguageService = new LanguageService(usersContext);
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
                var entity = await _UsersContext
                    .Users
                    .SingleOrDefaultAsync(usr => usr.Email.Equals(req.Email));
                if (entity != null)
                {
                    throw new Exception();
                }

                // Check if passwords match
                if (!req.Password.Equals(req.PasswordVerify))
                {
                    throw new Exception();
                }

                // Find status by name
                var statusName = Entities.StatusName.Pending.ToString();
                var status = _StatusService.FindByName(statusName);

                // Find role by name
                var roleName = Entities.RoleName.User.ToString();
                var role = _RoleService.FindByName(roleName);


                // Create new user
                entity = new Entities.User
                {
                    Email = req.Email,
                    Password = req.Password,
                    Status = status,
                    Role = role,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _UsersContext.Users.Add(entity);

                // Persist context
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.User(entity);
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
                var entities = await _UsersContext
                    .Users
                    .ToListAsync();
                var views = entities.Select(usr => new Views.Response.User(usr));
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
                var entity = await _UsersContext
                    .Users
                    .Include(usr => usr.Status)
                    .Include(usr => usr.Role)
                    .Include(usr => usr.Language)
                    .Include(usr => usr.Memberships)
                        .ThenInclude(mem => mem.Group)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // No entity was found
                if (entity == null)
                {
                    throw new Exception();
                }

                var view = new Views.Response.User(entity);

                return view;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    
        public async Task<Views.Response.User> Update(int id, Views.Request.Update req)
        {
            try
            {
                // Get entity from db
                var entity = await _UsersContext
                    .Users
                    .Include(usr => usr.Status)
                    .Include(usr => usr.Role)
                    .Include(usr => usr.Language)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // Update Status if Provided
                if (req.StatusCode != null)
                {
                    var status = _StatusService.FindByCode(req.StatusCode);
                    entity.Status = status;
                }

                // Update Role if Provided
                if (req.RoleCode != null)
                {
                    var role = _RoleService.FindByCode(req.RoleCode);
                    entity.Role = role;
                }

                // Update Language if Provided
                if (req.LanguageCode != null)
                {
                    var language = _LanguageService.FindByCode(req.LanguageCode);
                    entity.Language = language;
                }

                // Update Email if Provided
                if (req.Email != null)
                {
                    entity.Email = req.Email;
                }

                // Update FirstName if provided
                if (req.FirstName != null)
                {
                    entity.FirstName = req.FirstName;
                }

                // Update LastName if provided
                if (req.LastName != null)
                {
                    entity.LastName = req.LastName;
                }

                // Update Avatar if provided
                if (req.Avatar != null)
                {
                    entity.Avatar = req.Avatar;
                }

                // Set last time of update
                entity.LastUpdated = DateTime.UtcNow;

                // Update user
                _UsersContext
                    .Users
                    .Update(entity);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.User(entity);
                return view;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Views.Response.User> Deactivate(int id)
        {
            try
            {
                // Get entity from db
                var entity = await _UsersContext
                    .Users
                    .Include(usr => usr.Memberships)
                        .ThenInclude(mem => mem.Group)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // Find status by name
                var statusName = Entities.StatusName.Inactive.ToString();
                var status = _StatusService.FindByName(statusName);

                // Set status on user and memberships to inactive
                entity.Status = status;
                entity.Memberships.Select(mem => mem.Status = status);

                // Update user
                _UsersContext.Users.Update(entity);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.User(entity);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
