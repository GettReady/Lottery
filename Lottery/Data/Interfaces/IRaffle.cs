using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Interfaces
{
    public interface IRaffle
    {
        IEnumerable<Raffle> AllRaffles { get; }

        IEnumerable<Raffle> ActiveRaffles { get; }
        
        Raffle GetRaffle(int id);

        void SetRaffle(Raffle raffle);

        void DeleteRaffle(int raffleId);

        void FinishRaffle(int raffleId);

        void EditRaffle(Raffle newRaffle);

        IEnumerable<Raffle> GetUserRaffles(int userId);

        IEnumerable<User> GetWinners(Raffle raffle);

        string[] GetPrivateWinners(Raffle raffle);

        void AddParticipants(string raffleKey, string participants, char devider, int duplicateCheck);

        void SetParticipants(string raffleKey, string participants, char devider);
        
        void SetParticipants(string raffleKey, string participants);
    }
}
