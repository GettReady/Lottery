using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Models
{
    public class Prize
    {
        public int RaffleId { get; set; } //Внешний ключ
        public int Place { get; set; }
        public string PrizeDescription { get; set; }
        public int? WinnerId { get; set; }
        public string PrivateWinner { get; set; }

        public virtual Raffle Raffle { get; set; }

        public virtual User User { get; set; }
    }
}
