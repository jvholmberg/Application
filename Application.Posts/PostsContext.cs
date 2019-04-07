using System;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts
{
    public class PostsContext : DbContext
    {
        public PostsContext(DbContextOptions<PostsContext> options)
                : base(options) { }

        public DbSet<Entities.Post> Posts { get; set; }
        public DbSet<Entities.Comment> Comments { get; set; }
        public DbSet<Entities.Status> Statuses { get; set; }
        public DbSet<Entities.Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Post
            modelBuilder
                .Entity<Entities.Post>()
                .ToTable("post");
            modelBuilder
                .Entity<Entities.Post>()
                .HasOne(pst => pst.Status);

            // Comments
            modelBuilder
                .Entity<Entities.Comment>()
                .ToTable("comment");
            modelBuilder
                .Entity<Entities.Comment>()
                .HasOne(com => com.Status);
            modelBuilder
                .Entity<Entities.Comment>()
                .HasOne(com => com.Post)
                .WithMany(pst => pst.Comments);

            // Status
            modelBuilder
                .Entity<Entities.Status>()
                .ToTable("status");

            // Type
            modelBuilder
                .Entity<Entities.Type>()
                .ToTable("type");
        }
    }
}
