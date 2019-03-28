using System;
using System.Threading.Tasks;

namespace Application.Users.Services
{
    public interface IGroupService
    {
        Task<Views.Response.Group> Create(Views.Request.CreateGroup req);
        Task<Views.Response.Group> Update(int id, Views.Request.UpdateGroup req);
    }

    public class GroupService : IGroupService
    {

        private readonly UsersContext _UsersContext;

        private readonly IStatusService _StatusService;
        private readonly IRoleService _RoleService;
        private readonly ILanguageService _LanguageService;

        public GroupService(UsersContext usersContext)
        {
            _UsersContext = usersContext;

            _StatusService = new StatusService(usersContext);
            _RoleService = new RoleService(usersContext);
            _LanguageService = new LanguageService(usersContext);
        }

        public async Task<Views.Response.Group> Create(Views.Request.CreateGroup req)
        {
            try
            {
                // Check if name was provided
                if (string.IsNullOrWhiteSpace(req.Name))
                {
                    throw new Exception();
                }

                // Get user from context
                var user = await _UsersContext
                    .Users
                    .FindAsync(req.UserId);

                // Check if user exist
                if (user == null)
                {
                    throw new Exception();
                }

                // Find status by name
                var statusName = Entities.StatusName.Active.ToString();
                var status = _StatusService.FindByName(statusName);

                // Find role by name
                var roleName = Entities.RoleName.Admin.ToString();
                var role = _RoleService.FindByName(roleName);

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

        public async Task<Views.Response.Group> Update(int id, Views.Request.UpdateGroup req)
        {
            try
            {
                // Get group from context
                var entity = await _UsersContext
                    .Groups
                    .FindAsync(id);

                // Check if group exists
                if (entity == null)
                {
                    throw new Exception();
                }

                // Update Name if provided
                if (req.Name != null)
                {
                    entity.Name = req.Name;
                }

                // Set last time of update
                entity.LastUpdated = DateTime.UtcNow;

                // Update group
                _UsersContext
                    .Groups
                    .Update(entity);

                // Persist changes
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Group(entity);
                return view;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
