using Lottery.Data.Interfaces;
using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Repository
{
    public class UserRaffleRepository : IUserRaffle
    {
        private LotteryDbContext dbContext;

        public UserRaffleRepository(LotteryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<UserRaffle> UserRaffles => throw new NotImplementedException();

        public IEnumerable<UserRaffle> RaffleParticipants => throw new NotImplementedException();

        public void AddParticipant(int raffleId, int userId)
        {
            UserRaffle ur = dbContext.UserRaffle.FirstOrDefault(c => c.RaffleId == raffleId && c.UserId == userId);
            if(ur==null)
            {
                if(dbContext.Raffle.FirstOrDefault(c => c.Id==raffleId)!=null && dbContext.User.FirstOrDefault(c => c.Id == userId) != null)
                {
                    UserRaffle newUseRaffle = new UserRaffle() { RaffleId = raffleId, UserId = userId, Status = "participant" };
                    dbContext.UserRaffle.Add(newUseRaffle);
                    dbContext.SaveChanges();
                }
            }
        }

        public void SetAuthor(int raffleId, int userId)
        {
            if(dbContext.User.Where(c=>c.Id==userId).Any() && dbContext.Raffle.Where(c => c.Id == raffleId).Any())
            {
                dbContext.UserRaffle.Add(new UserRaffle { UserId = userId, RaffleId = raffleId, Status = "author" });
            }
        }

        public void SetParticipant(int raffleId, int userId)
        {
            if (dbContext.User.Where(c => c.Id == userId).Any() && dbContext.Raffle.Where(c => c.Id == raffleId).Any())
            {
                if (!dbContext.UserRaffle.Where(c => c.UserId == userId).Any())
                {
                    dbContext.UserRaffle.Add(new UserRaffle { UserId = userId, RaffleId = raffleId, Status = "participant" });
                }
            }
        }

        public void SetWinner(int raffleId, int userId)
        {
            var userRaffle = dbContext.UserRaffle.FirstOrDefault(c => c.UserId == userId);
            if(userRaffle != null && userRaffle.RaffleId==raffleId && userRaffle.Status!="author")
            {
                userRaffle.Status = "winner";
            }
        }
    }
}
