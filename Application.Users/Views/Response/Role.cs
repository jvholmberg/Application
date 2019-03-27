﻿using System;
namespace Application.Users.Views.Response
{
    public class Role
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Role(Entities.Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}
