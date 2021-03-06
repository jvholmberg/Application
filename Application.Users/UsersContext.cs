﻿using System;
using Microsoft.EntityFrameworkCore;

namespace Application.Users
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options)
                : base(options) { }

        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Group> Groups { get; set; }
        public DbSet<Entities.Membership> Memberships { get; set; }
        public DbSet<Entities.Language> Languages { get; set; }
        public DbSet<Entities.Role> Roles { get; set; }
        public DbSet<Entities.Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder
                .Entity<Entities.User>()
                .ToTable("user");
            modelBuilder
                .Entity<Entities.User>()
                .HasOne(usr => usr.Status);
            modelBuilder
                .Entity<Entities.User>()
                .HasOne(usr => usr.Role);
            modelBuilder
                .Entity<Entities.User>()
                .HasOne(usr => usr.Language);

            // Group
            modelBuilder
                .Entity<Entities.Group>()
                .ToTable("group");
            modelBuilder
                .Entity<Entities.Group>()
                .HasOne(grp => grp.Status);

            // Membership
            modelBuilder
                .Entity<Entities.Membership>()
                .ToTable("membership");
            modelBuilder
                .Entity<Entities.Membership>()
                .HasOne(mem => mem.Status);
            modelBuilder
                .Entity<Entities.Membership>()
                .HasOne(mem => mem.Role);
            modelBuilder
                .Entity<Entities.Membership>()
                .HasOne(mem => mem.User)
                .WithMany(usr => usr.Memberships);
            modelBuilder
                .Entity<Entities.Membership>()
                .HasOne(mem => mem.Group)
                .WithMany(grp => grp.Memberships);

            // Language
            modelBuilder
                .Entity<Entities.Language>()
                .ToTable("language");

            // Role
            modelBuilder
                .Entity<Entities.Role>()
                .ToTable("role");

            // Status
            modelBuilder
                .Entity<Entities.Status>()
                .ToTable("status");
        }
    }
}
