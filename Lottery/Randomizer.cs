using Lottery.Data;
using Lottery.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Lottery.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data.Models;

namespace Lottery
{
    public class Randomizer
    {
        static Random rand = new Random();

        private readonly IRaffle Raffle;
        //private readonly IUserRaffle UserRaffle;

        public Randomizer(IRaffle raffle/*, IUserRaffle userRaffle*/)
        {
            Raffle = raffle;
            //UserRaffle = userRaffle;
        }

        public void CheckRaffles()
        {
            //LotteryDbContext dbContext = new LotteryDbContext();


            //var activeRaffles = dbContext.Raffle.Where(c => c.Expired == false);


            //var activeRaffles = Raffle.ActiveRaffles;
            List<Raffle> activeRaffles = Raffle.ActiveRaffles.ToList();
            foreach (Raffle raffle in activeRaffles)
            {
                //using (StreamWriter writetext = new StreamWriter("write.txt", true))
                //{
                //    writetext.WriteLine(raffle.Id+"\n");
                //}

                if (raffle.ExpirationTime <= DateTime.Now)
                {
                    //using (StreamWriter writetext = new StreamWriter("write.txt", true))
                    //{
                    //    writetext.WriteLine(raffle.Id + "Raffle finished!\n");
                    //}

                    Raffle.FinishRaffle(raffle.Id);


                    //raffle.Expired = true;
                    //List<int> winners = GetWinners(dbContext.UserRaffle.Where(c => c.RaffleId == raffle.Id && c.Status == "participant").ToList(), raffle.Places);
                    //for (int i = 0; i < raffle.Places; ++i)
                    //{
                    //    dbContext.Prize.Where(c => c.RaffleId == raffle.Id && c.Place == i + 1).First().WinnerId = winners[i];
                    //    dbContext.UserRaffle.Where(c => c.UserId == winners[i] && c.RaffleId == raffle.Id).First().Status = "winner";
                    //}
                    //dbContext.SaveChanges();
                }
            }

            //using (StreamWriter writetext = new StreamWriter("write.txt", true))
            //{
            //    writetext.WriteLine("end\n\n\n");
            //}

        }

        public static List<int> GetWinners(/*List<UserRaffle> participants*/List<int> participants, int places)
        {
            List<int> winners = new List<int>();
            int winner;

            for (int i=0; i< places; ++i)
            {
                if (participants.Count() != 0)
                {
                    winner = rand.Next(participants.Count());
                    //winners.Add(participants[winner].UserId);
                    winners.Add(participants[winner]);
                    participants.RemoveAt(winner);
                }                
            }

            return winners;
        }

        public static string GenerateApiKey()
        {            
            string key = "";
            int charDec;
            for(int i=0; i<16; ++i)
            {
                charDec = rand.Next(48, 122);
                while ((charDec <= 96 && charDec >= 91) || (charDec >= 58 && charDec <= 64))
                {
                    charDec = rand.Next(48, 122);
                }
                key += (char)charDec;
            }

            return key;
        }
    }
}
