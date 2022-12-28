using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.ViewModels
{
    public class RafflesUserViewModel
    {
        public IEnumerable<Raffle> Raffles { get; set; }
        public User User { get; set; }
        public IEnumerable<Prize> Prizes { get; set; }
        public IEnumerable<User> Winners { get; set; }
        public string[] PrivateWinners { get; set; }
        public int[] UserRaffles { get; set; }
        //public string Participants { get; set; }
    }
}
