using Lottery.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data
{
    public class LotteryDbContext : DbContext
    {
        public LotteryDbContext(DbContextOptions<LotteryDbContext> options) : base(options)
        {            
        }

        public LotteryDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Raffle> Raffle { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRaffle> UserRaffle { get; set; }
        //public DbSet<PrivateRafflesUserList> PrivateRafflesUserList { get; set; }
        public DbSet<Prize> Prize { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(q => q.Id);
            builder.Entity<Raffle>().HasKey(q => q.Id);            
            builder.Entity<UserRaffle>().HasKey(q => new { q.UserId, q.RaffleId });
            //builder.Entity<PrivateRafflesUserList>().HasKey(q => q.RaffleId);
            builder.Entity<Prize>().HasKey(q => new { q.RaffleId, q.Place });            

            builder.Entity<UserRaffle>()
                .HasOne(t => t.Raffle)
                .WithMany(t => t.Participants)
                .HasForeignKey(t => t.RaffleId);

            builder.Entity<UserRaffle>()
                .HasOne(t => t.User)
                .WithMany(t => t.Participations)
                .HasForeignKey(t => t.UserId);

            //builder.Entity<PrivateRafflesUserList>()
            //    .HasOne(t => t.Raffle)                
            //    .WithOne(t => t.PrivateParticipants)
            //    .HasForeignKey(t => t.RaffleId);

            builder.Entity<Prize>()
                .HasOne(t => t.User)
                .WithMany(t => t.Prizes)
                .HasForeignKey(t => t.WinnerId);
        }
    }
}
