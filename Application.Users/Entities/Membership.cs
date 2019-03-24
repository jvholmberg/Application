﻿using System;
namespace Application.Users.Entities
{
    public class Membership
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }

        public Group Group { get; set; }

        public Membership()
        {
        }
    }
}
