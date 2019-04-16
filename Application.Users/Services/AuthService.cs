using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Users.Services
{
    public interface IAuthService
    {
        Task<Views.Response.Auth> Validate(Views.Request.ValidateAuth req);
        Task<Views.Response.Auth> Refresh(int userId, string accessToken, string refreshToken);
    }

    public class AuthService : IAuthService
    {

        private readonly UsersContext _UsersContext;
        private readonly IBaseService _BaseService;
        private readonly UsersSettings _UsersSettings;

        public AuthService(IOptions<UsersSettings> usersSettings, UsersContext usersContext)
        {
            _UsersContext = usersContext;
            _BaseService = new BaseService(usersContext);
            _UsersSettings = usersSettings.Value;
        }

        public async Task<Views.Response.Auth> Validate(Views.Request.ValidateAuth req)
        {
            try
            {
                // Check if data was provided
                if (string.IsNullOrWhiteSpace(req.Email)
                    || string.IsNullOrWhiteSpace(req.Password))
                {
                    throw new Core
                        .Exceptions
                        .InvalidArgumentsException("Email or password is empty");
                }

                // Check if user with email exists
                var user = await _UsersContext
                    .Users
                    .Include(usr => usr.Status)
                    .Include(usr => usr.Role)
                    .SingleOrDefaultAsync(usr => usr.Email.Equals(req.Email));

                if (user == null)
                {
                    throw new Core
                        .Exceptions
                        .NotFoundException("User with email does not exists");
                }

                if(user.Password != req.Password)
                {
                    throw new Core
                        .Exceptions
                        .InvalidArgumentsException("Incorrect password");
                }

                // Find status by name and update user
                var statusName = Entities.StatusName.Active.ToString();
                var status = _BaseService.FindStatusByName(statusName);
                user.Status = status;

                // Create new accessToken
                var expiry = DateTime.UtcNow.AddDays(3);
                var accessToken = CreateAccessToken(user, expiry);

                // Create new refreshToken
                var refreshToken = CreateRefreshToken();
                user.RefreshToken = refreshToken;

                _UsersContext.Users.Update(user);

                // Persist context
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Auth(accessToken, expiry, user);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Views.Response.Auth> Refresh(int userId, string accessToken, string refreshToken)
        {
            try
            {
                // Check if data was provided
                if (string.IsNullOrEmpty(refreshToken))
                {
                    throw new Core
                        .Exceptions
                        .InvalidArgumentsException("No refreshtoken");
                }

                // Check if user exists
                var user = await _UsersContext
                    .Users
                    .Include(usr => usr.Status)
                    .Include(usr => usr.Role)
                    .SingleOrDefaultAsync(usr => usr.Id.Equals(userId));

                if (user == null)
                {
                    throw new Core
                        .Exceptions
                        .UnauthorizedException("Invalid refreshToken");
                }

                // Check if user active
                if (user.Status.Name != Entities.StatusName.Active.ToString())
                {
                    throw new Core
                        .Exceptions
                        .UnauthorizedException("User inactive");
                }

                // Check if allowed               
                if (user.RefreshToken != refreshToken)
                {
                    throw new Core
                        .Exceptions
                        .UnauthorizedException("Invalid refreshToken");
                }

                // Create new accessToken
                var expiry = DateTime.UtcNow.AddDays(3);
                var newAccessToken = CreateAccessToken(user, expiry);

                // Create new refreshToken
                var newRefreshToken = CreateRefreshToken();
                user.RefreshToken = newRefreshToken;

                // Persist context
                _UsersContext.Users.Update(user);
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Auth(newAccessToken, expiry, user);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateAccessToken(Entities.User user, DateTime expiry)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_UsersSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                }),
                Expires = expiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securitytoken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securitytoken);
            return accessToken;
        }

        public string CreateRefreshToken()
        {
            var refreshToken = Guid.NewGuid().ToString();
            return refreshToken;
        }
    }
}
