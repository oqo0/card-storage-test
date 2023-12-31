﻿// <auto-generated />
using System;
using CardStorage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CardStorage.Data.Migrations
{
    [DbContext(typeof(CardStorageDbContext))]
    partial class CardStorageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CardStorage.Data.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("varchar(127)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("varchar(127)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CardStorage.Data.Models.AccountSessions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SessionToken")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<DateTime>("TimeClosed")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountSessions");
                });

            modelBuilder.Entity("CardStorage.Data.Models.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CVV2")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime>("ExpDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<ulong>("OwnerId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CardStorage.Data.Models.User", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SureName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CardStorage.Data.Models.AccountSessions", b =>
                {
                    b.HasOne("CardStorage.Data.Models.Account", "Account")
                        .WithMany("Sessions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CardStorage.Data.Models.Card", b =>
                {
                    b.HasOne("CardStorage.Data.Models.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardStorage.Data.Models.Account", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("CardStorage.Data.Models.User", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
