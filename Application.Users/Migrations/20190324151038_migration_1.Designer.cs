﻿// <auto-generated />
using System;
using Application.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Application.Users.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20190324151038_migration_1")]
    partial class migration_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Application.Users.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("group");
                });

            modelBuilder.Entity("Application.Users.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("language");
                });

            modelBuilder.Entity("Application.Users.Entities.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("GroupId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int?>("RoleId");

                    b.Property<int?>("StatusId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("membership");
                });

            modelBuilder.Entity("Application.Users.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Application.Users.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("status");
                });

            modelBuilder.Entity("Application.Users.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int?>("LanguageId");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Password");

                    b.Property<string>("RefreshToken");

                    b.Property<DateTime>("RefreshTokenExpiry");

                    b.Property<int>("RefreshTokenLifetime");

                    b.Property<int?>("RoleId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Application.Users.Entities.Group", b =>
                {
                    b.HasOne("Application.Users.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("Application.Users.Entities.Membership", b =>
                {
                    b.HasOne("Application.Users.Entities.Group", "Group")
                        .WithMany("Memberships")
                        .HasForeignKey("GroupId");

                    b.HasOne("Application.Users.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Application.Users.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("Application.Users.Entities.User", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Application.Users.Entities.User", b =>
                {
                    b.HasOne("Application.Users.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("Application.Users.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Application.Users.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });
#pragma warning restore 612, 618
        }
    }
}
