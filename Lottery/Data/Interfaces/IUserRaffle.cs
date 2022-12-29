using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Interfaces
{
    public interface IUserRaffle
    {
        IEnumerable<UserRaffle> UserRaffles { get; }

        IEnumerable<UserRaffle> RaffleParticipants { get; }

        void SetAuthor(int raffleId, int userId);

        void AddParticipant(int raffleId, int userId);
    }
}
