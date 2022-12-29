using Lottery.Data.Interfaces;
using Lottery.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Lottery.Data.Repository
{
    public class RaffleRepository : IRaffle
    {
        private LotteryDbContext dbContext;
        
        public RaffleRepository(LotteryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Raffle> AllRaffles => dbContext.Raffle;

        public IEnumerable<Raffle> ActiveRaffles => dbContext.Raffle.Where(c => c.Expired == false);

        public void DeleteRaffle(int raffleId)
        {
            var raffle = dbContext.Raffle.FirstOrDefault(c => c.Id == raffleId);
            dbContext.Raffle.Remove(raffle);
            dbContext.SaveChanges();
        }

        public void EditRaffle(Raffle newRaffle)
        {
            var editRaffle = dbContext.Raffle.SingleOrDefault(c => c.Id == newRaffle.Id);
            
            editRaffle.Title = newRaffle.Title;
            editRaffle.ExpirationTime = newRaffle.ExpirationTime;
            editRaffle.Expired = newRaffle.Expired;
            editRaffle.Description = newRaffle.Description;
            editRaffle.FullDescription = newRaffle.FullDescription;

            dbContext.SaveChanges();
        }

        public void FinishRaffle(int raffleId)
        {
            Raffle expiredRaffle = dbContext.Raffle.SingleOrDefault(c => c.Id == raffleId);
            expiredRaffle.Expired = true;

            List<int> winners = new List<int>();
            List<string> privateWinners = new List<string>();
            List<string> privateParticipants = new List<string>();
            if (expiredRaffle.IsPrivate)
            {
                List<int> participantsOrder = new List<int>();
                privateParticipants = expiredRaffle.PrivateParticipants.Split('\n').ToList();
                privateParticipants.RemoveAll(c => c.Contains('\n') || c.Contains(' ') || c == "" || c == null);
                for (int i=0; i<privateParticipants.Count; ++i)
                {
                    participantsOrder.Add(i);
                }
                winners = Randomizer.GetWinners(participantsOrder, expiredRaffle.Places);
            }
            else
            {
                winners = Randomizer.GetWinners(dbContext.UserRaffle.Where(c => c.RaffleId == raffleId && c.Status == "participant").Select(c => c.RaffleId).ToList(), expiredRaffle.Places);
            }

            if (winners != null && winners.Count() != 0)
            {
                if (expiredRaffle.IsPrivate)
                {
                    for (int i = 0; i < expiredRaffle.Places; ++i)
                    {
                        dbContext.Prize.FirstOrDefault(c => c.RaffleId == expiredRaffle.Id && c.Place == i + 1).PrivateWinner = privateParticipants[winners[i]];
                    }
                }
                else
                {
                    for (int i = 0; i < expiredRaffle.Places; ++i)
                    {
                        dbContext.Prize.Where(c => c.RaffleId == expiredRaffle.Id && c.Place == i + 1).First().WinnerId = winners[i];
                        dbContext.UserRaffle.Where(c => c.UserId == winners[i] && c.RaffleId == expiredRaffle.Id).First().Status = "winner";
                    }
                }
            }
            dbContext.SaveChanges();
        }

        public Raffle GetRaffle(int id) => dbContext.Raffle.FirstOrDefault(c => c.Id == id);

        public IEnumerable<Raffle> GetUserRaffles(int userId)
        {
            IEnumerable<UserRaffle> userRaffles = dbContext.UserRaffle.Where(c => c.UserId == userId);
            IEnumerable<int> rafflesIds = userRaffles.Select(c => c.RaffleId);
            return dbContext.Raffle.Where(c => rafflesIds.Contains(c.Id));
        }

        public IEnumerable<User> GetWinners(Raffle raffle)
        {
            IEnumerable<UserRaffle> userRaffles = dbContext.UserRaffle.Where(c => c.RaffleId == raffle.Id && c.Status == "winner");
            IEnumerable<int> winnersIds = userRaffles.Select(c => c.UserId);
            return dbContext.User.Where(c => winnersIds.Contains(c.Id));
        }

        public void SetRaffle(Raffle raffle)
        {
            dbContext.Raffle.Add(raffle);
            dbContext.SaveChanges();
        }

        public void AddParticipants(string raffleKey, string participants, char devider, int duplicateCheck = 0)
        {            
            if (dbContext.Raffle.Where(c => c.Key == raffleKey).Any())
            {
                Raffle raffle = dbContext.Raffle.SingleOrDefault(c => c.Key == raffleKey);                
                List<string> newParticipantsArr = participants.Split(devider).ToList();
                newParticipantsArr.RemoveAll(c => c.Contains('\n') || c.Contains(' ') || c == "" || c == null);
                string[] currentParticipants = raffle.PrivateParticipants.Split('\n');

                if (currentParticipants.Count() != 0)
                {
                    if (duplicateCheck == 1)
                    {
                        for (int i = 0; i < currentParticipants.Count(); ++i)
                        {
                            if (newParticipantsArr.Contains(currentParticipants[i]))
                            {
                                newParticipantsArr.Remove(currentParticipants[i]);
                            }
                        }
                    }
                }
                string newParticipants = string.Join('\n', currentParticipants);
                foreach(string name in newParticipantsArr)
                {
                    newParticipants += name + '\n';
                }
                newParticipants = newParticipants.Remove(newParticipants.Length - 1);

                raffle.PrivateParticipants = newParticipants;
                dbContext.SaveChanges();
            }
        }

        public string GetParticipants(int id)
        {            
            string participants = dbContext.Raffle.SingleOrDefault(c => c.Id == id).PrivateParticipants;         
            return participants;
        }

        public void SetParticipants(string raffleKey, string participants, char devider)
        {
            Raffle raffle = dbContext.Raffle.SingleOrDefault(c => c.Key == raffleKey);
            string newParticipants = string.Join('\n', participants.Split(devider));
            raffle.PrivateParticipants = newParticipants;
            dbContext.SaveChanges();                        
        }

        public void SetParticipants(string raffleKey, string participants)
        {
            throw new NotImplementedException();
        }

        public string[] GetPrivateWinners(Raffle raffle)
        {
            string[] winners = new string[raffle.Places];
            List<Prize> prizes = dbContext.Prize.Where(c => c.RaffleId == raffle.Id).ToList();

            for (int i=0; i<raffle.Places; ++i)
            {
                winners[i] = prizes.FirstOrDefault(c => c.Place == i + 1).PrivateWinner;
            }

            return winners;
        }
    }
}
