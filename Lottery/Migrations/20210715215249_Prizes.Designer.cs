﻿// <auto-generated />
using System;
using Lottery.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lottery.Migrations
{
    [DbContext(typeof(LotteryDbContext))]
    [Migration("20210715215249_Prizes")]
    partial class Prizes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lottery.Data.Models.Prize", b =>
                {
                    b.Property<int>("RaffleId")
                        .HasColumnType("int");

                    b.Property<int>("Placement")
                        .HasColumnType("int");

                    b.Property<int>("PrizeDescription")
                        .HasColumnType("int");

                    b.HasKey("RaffleId", "Placement");

                    b.ToTable("Prize");
                });

            modelBuilder.Entity("Lottery.Data.Models.Raffle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Expired")
                        .HasColumnType("bit");

                    b.Property<string>("FullDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Places")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Raffle");
                });

            modelBuilder.Entity("Lottery.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Lottery.Data.Models.UserRaffle", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RaffleId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RaffleId");

                    b.HasIndex("RaffleId");

                    b.ToTable("UserRaffle");
                });

            modelBuilder.Entity("Lottery.Data.Models.Prize", b =>
                {
                    b.HasOne("Lottery.Data.Models.Raffle", "Raffle")
                        .WithMany("Prizes")
                        .HasForeignKey("RaffleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Raffle");
                });

            modelBuilder.Entity("Lottery.Data.Models.UserRaffle", b =>
                {
                    b.HasOne("Lottery.Data.Models.Raffle", "Raffle")
                        .WithMany("Participants")
                        .HasForeignKey("RaffleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lottery.Data.Models.User", "User")
                        .WithMany("Participations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Raffle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lottery.Data.Models.Raffle", b =>
                {
                    b.Navigation("Participants");

                    b.Navigation("Prizes");
                });

            modelBuilder.Entity("Lottery.Data.Models.User", b =>
                {
                    b.Navigation("Participations");
                });
#pragma warning restore 612, 618
        }
    }
}
