using Lottery.Data.Interfaces;
using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Repository
{
    public class PrizeRepository : IPrize
    {
        private LotteryDbContext dbContext;

        public PrizeRepository(LotteryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Prize> AllPrizes => dbContext.Prize;

        public void EditPrizes(int raffleId, List<Prize> prizes)
        {

            //dbContext.Prize.RemoveRange(dbContext.Prize.Where(c => c.RaffleId == raffleId));
            //dbContext.Prize.AddRange(prizes);
            var edit = dbContext.Prize.Where(c => c.RaffleId == raffleId).ToList();

            for (int i = 0; i < prizes.Count(); ++i)
            {
                edit[i].PrizeDescription = prizes[i].PrizeDescription;
            }
            dbContext.SaveChanges();
        }

        public IEnumerable<Prize> GetPrizes(int raffleId) => dbContext.Prize.Where(c => c.RaffleId == raffleId);

        public void SetPrizes(List<Prize> prizes)
        {
            for(int i=0; i<prizes.Count(); ++i)
            {
                prizes[i].Place = i + 1;
                //prizes[i].RaffleId=
            }
            dbContext.Prize.AddRange(prizes);
            dbContext.SaveChanges();
        }
    }
}
