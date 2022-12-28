using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Interfaces
{
    public interface IPrize
    {
        IEnumerable<Prize> AllPrizes { get; }

        IEnumerable<Prize> GetPrizes(int raffleId);

        void SetPrizes(List<Prize> prizes);

        void EditPrizes(int raffleId, List<Prize> prizes);

        //void SetWinner(int raffleId, int userId, int place);
    }
}
