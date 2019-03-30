using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Entities
{
    public class Role
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public enum RoleName
    {
        Admin,
        User
    }

    public enum RoleCode
    {
        ADMIN,
        USER
    }
}
