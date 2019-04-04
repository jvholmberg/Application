using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Services
{
    public interface IGroupService
    {
        Task<Views.Response.Group> Create(Views.Request.CreateGroup req);
        Task<IEnumerable<Views.Response.Group>> GetAll();
        Task<Views.Response.Group> GetById(int id);
        Task<Views.Response.Group> Update(int id, Views.Request.UpdateGroup req);
        Task<Views.Response.Group> Delete(int id);
    }

    public class GroupService : IGroupService
    {

        private readonly UsersContext _UsersContext;
        private readonly IBaseService _BaseService;

        public GroupService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
            _BaseService = new BaseService(usersContext);
        }

        public async Task<Views.Response.Group> Create(Views.Request.CreateGroup req)
        {
            try
            {
                // Check if name was provided
                if (string.IsNullOrWhiteSpace(req.Name))
                {
                    throw new Core.Exceptions.InvalidArgumentsException();
                }

                // Get group from context
                var existingGroup = await _UsersContext
                    .Groups
                    .SingleOrDefaultAsync(grp => grp.Name.Equals(req.Name));

                // Check if group with name already exists
                if (existingGroup != null)
                {
                    throw new Core.Exceptions.ExistingFoundException();
                }

                // Get user from context
                var user = await _UsersContext
                    .Users
                    .FindAsync(req.UserId);

                // Check if user exist
                if (user == null)
                {
                    throw new Core.Exceptions.InvalidArgumentsException();
                }

                // Find status by name
                var statusName = Entities.StatusName.Active.ToString();
                var status = _BaseService.FindStatusByName(statusName);

                // Find role by name
                var roleName = Entities.RoleName.Admin.ToString();
                var role = _BaseService.FindRoleByName(roleName);


                // Create Group
                var group = new Entities.Group
                {
                    Name = req.Name,
                    Status = status,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _UsersContext.Groups.Add(group);

                // Create Membership
                var membership = new Entities.Membership
                {
                    Group = group,
                    User = user,
                    Status = status,
                    Role = role,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _UsersContext.Memberships.Add(membership);

                // Persist context
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Group(group);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Views.Response.Group>> GetAll()
        {
            try
            {
                var groups = await _UsersContext
                    .Groups
                    .ToListAsync();
                var views = groups.Select(grp => new Views.Response.Group(grp));
                return views;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Views.Response.Group> GetById(int id)
        {
            try
            {
                // Get entity from db
                var group = await _UsersContext
                    .Groups
                    .Include(grp => grp.Status)
                    .Include(grp => grp.Memberships)
                        .ThenInclude(mem => mem.User)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                // No entity was found
                if (group == null)
                {
                    throw new Core.Exceptions.NotFoundException();
                }

                var view = new Views.Response.Group(group);

                return view;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Views.Response.Group> Update(int id, Views.Request.UpdateGroup req)
        {
            try
            {
                // Get group from context
                var group = await _UsersContext
                    .Groups
                    .FindAsync(id);

                // Check if group exists
                if (group == null)
                {
                    throw new Core.Exceptions.NotFoundException();
                }

                // Update Name if provided
                if (req.Name != null)
                {
                    group.Name = req.Name;
                }

                // Set last time of update
                group.LastUpdated = DateTime.UtcNow;

                // Update group
                _UsersContext
                    .Groups
                    .Update(group);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Group(group);
                return view;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Views.Response.Group> Delete(int id)
        {
            try
            {
                // Get group from db
                var group = await _UsersContext
                    .Groups
                    .Include(grp => grp.Memberships)
                        .ThenInclude(mem => mem.User)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(id));

                if (group == null)
                {
                    throw new Core.Exceptions.NotFoundException();
                }

                // Find status by name
                var statusName = Entities.StatusName.Inactive.ToString();
                var status = _BaseService.FindStatusByName(statusName);

                // Set status on group and memberships to inactive
                group.Status = status;
                group.LastUpdated = DateTime.UtcNow;
                group.Memberships.Select(mem => mem.Status = status);

                // Update group
                _UsersContext.Groups.Update(group);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Group(group);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
