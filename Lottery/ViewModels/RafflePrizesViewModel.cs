using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.ViewModels
{
    public class RafflePrizesViewModel
    {
        public Raffle Raffle { get; set; }
        public List<Prize> Prizes { get; set; }
        public DateTime Time { get; set; }
    }
}
