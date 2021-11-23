﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _netCourse.Data;

namespace _netCourse.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211119030241_FightProperties")]
    partial class FightProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CharacterSkills", b =>
                {
                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<int>("charactersId")
                        .HasColumnType("int");

                    b.HasKey("SkillId", "charactersId");

                    b.HasIndex("charactersId");

                    b.ToTable("CharacterSkills");
                });

            modelBuilder.Entity("_netCourse.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("Fights")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Victories")
                        .HasColumnType("int");

                    b.Property<int>("defeats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("characters");
                });

            modelBuilder.Entity("_netCourse.Models.Skills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("damage")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            damage = 30,
                            name = "Fireball"
                        },
                        new
                        {
                            Id = 2,
                            damage = 20,
                            name = "Frenzy"
                        },
                        new
                        {
                            Id = 3,
                            damage = 50,
                            name = "Blizzard"
                        });
                });

            modelBuilder.Entity("_netCourse.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("_netCourse.Weapons", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("weapons");
                });

            modelBuilder.Entity("CharacterSkills", b =>
                {
                    b.HasOne("_netCourse.Models.Skills", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_netCourse.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("charactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_netCourse.Models.Character", b =>
                {
                    b.HasOne("_netCourse.Models.User", "User")
                        .WithMany("characters")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_netCourse.Weapons", b =>
                {
                    b.HasOne("_netCourse.Models.Character", "character")
                        .WithOne("weapon")
                        .HasForeignKey("_netCourse.Weapons", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("character");
                });

            modelBuilder.Entity("_netCourse.Models.Character", b =>
                {
                    b.Navigation("weapon");
                });

            modelBuilder.Entity("_netCourse.Models.User", b =>
                {
                    b.Navigation("characters");
                });
#pragma warning restore 612, 618
        }
    }
}