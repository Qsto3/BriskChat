﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TrollChat.DataAccess.Context;

namespace TrollChat.DataAccess.Migrations
{
    [DbContext(typeof(TrollChatDbContext))]
    [Migration("20170322130927_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrollChat.DataAccess.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("UserRoomId");

                    b.HasKey("Id");

                    b.HasIndex("UserRoomId")
                        .IsUnique();

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("Customization");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<bool>("IsPrivateConversation");

                    b.Property<bool>("IsPublic");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<int>("OwnerId");

                    b.Property<string>("Topic")
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.RoomTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int?>("RoomId");

                    b.Property<int?>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("TagId");

                    b.ToTable("RoomTags");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<DateTime?>("EmailConfirmedOn");

                    b.Property<DateTime?>("LockedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(128)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.UserRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime?>("LockedUntil");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("RoleId");

                    b.Property<int>("RoomId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRooms");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.UserRoomTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int?>("TagId");

                    b.Property<int?>("UserRoomId");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserRoomId");

                    b.ToTable("UserRoomTags");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.Message", b =>
                {
                    b.HasOne("TrollChat.DataAccess.Models.UserRoom", "UserRoom")
                        .WithOne("LastMessage")
                        .HasForeignKey("TrollChat.DataAccess.Models.Message", "UserRoomId");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.Room", b =>
                {
                    b.HasOne("TrollChat.DataAccess.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.RoomTag", b =>
                {
                    b.HasOne("TrollChat.DataAccess.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("TrollChat.DataAccess.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.UserRoom", b =>
                {
                    b.HasOne("TrollChat.DataAccess.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("TrollChat.DataAccess.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("TrollChat.DataAccess.Models.User", "User")
                        .WithMany("Rooms")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TrollChat.DataAccess.Models.UserRoomTag", b =>
                {
                    b.HasOne("TrollChat.DataAccess.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.HasOne("TrollChat.DataAccess.Models.UserRoom", "UserRoom")
                        .WithMany()
                        .HasForeignKey("UserRoomId");
                });
        }
    }
}
