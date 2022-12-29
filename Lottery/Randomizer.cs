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

        public Randomizer(IRaffle raffle)
        {
            Raffle = raffle;
        }

        public void CheckRaffles()
        {
            List<Raffle> activeRaffles = Raffle.ActiveRaffles.ToList();
            foreach (Raffle raffle in activeRaffles)
            {
                if (raffle.ExpirationTime <= DateTime.Now)
                {
                    Raffle.FinishRaffle(raffle.Id);
                }
            }
        }

        public static List<int> GetWinners(List<int> participants, int places)
        {
            List<int> winners = new List<int>();
            int winner;

            for (int i=0; i< places; ++i)
            {
                if (participants.Count() != 0)
                {
                    winner = rand.Next(participants.Count());
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
