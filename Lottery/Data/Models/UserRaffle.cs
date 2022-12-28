using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Models
{
    public class UserRaffle
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RaffleId { get; set; }
        public virtual Raffle Raffle { get; set; }

        [RegularExpression("author|participant|winner", ErrorMessage = "The status value must be either 'author', 'participant' or 'winner' only.")]
        public string Status { get; set; }  // author, participant, winner
    }
}
