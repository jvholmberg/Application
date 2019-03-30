﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public AuthService(UsersContext usersContext)
        {
            _UsersContext = usersContext;
            _BaseService = new BaseService(usersContext);
        }

        public async Task<Views.Response.Auth> Validate(Views.Request.ValidateAuth req)
        {
            try
            {
                // Check if data was provided
                if (string.IsNullOrWhiteSpace(req.Email)
                    || string.IsNullOrWhiteSpace(req.Password))
                {
                    throw new Exception();
                }

                // Check if user with email exists
                var entity = await _UsersContext
                    .Users
                    .SingleOrDefaultAsync(usr => usr.Email.Equals(req.Email));

                if (entity == null)
                {
                    throw new Exception();
                }

                if(entity.Password != req.Password)
                {
                    throw new Exception();
                }

                // Find status by name
                var statusName = Entities.StatusName.Pending.ToString();
                var status = _BaseService.FindStatusByName(statusName);

                // Update user
                entity.Status = status;
                // TODO: generate new accessToken
                var accessToken = "";
                // TODO: generate new refreshToken

                _UsersContext.Users.Update(entity);

                // Persist context
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Auth(accessToken, entity);
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
                    throw new Exception();
                }

                // Check if user exists
                var user = await _UsersContext
                    .Users
                    .FindAsync(userId);

                if (user == null)
                {
                    throw new Exception();
                }

                /*
                // Check if allowed               
                if (user.AccessToken != accessToken
                    || user.RefreshToken != refreshToken)
                {
                    throw new Exception();
                }
                */

                // TODO: generate new accessToken
                var newAccessToken = "";
                // TODO: generate new refreshToken
                var newRefreshToken = "";

                // user.AcessToken = newAccessToken;
                user.RefreshToken = newRefreshToken;

                _UsersContext.Users.Update(user);

                // Persist context
                await _UsersContext.SaveChangesAsync();

                var view = new Views.Response.Auth(accessToken, user);
                return view;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
