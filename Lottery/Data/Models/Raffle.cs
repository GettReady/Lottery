using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Models
{
    public class Raffle
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FullDescription { get; set; }

        public int Places { get; set; }

        public DateTime ExpirationTime { get; set; }

        public bool Expired { get; set; }

        ///////////////////////////////////////////////////////
        public bool IsPrivate { get; set; }
        public string Key { get; set; }
        public string PrivateParticipants { get; set; }
        ///////////////////////////////////////////////////////        

        public int AuthorId { get; set; }        

        public virtual List<UserRaffle> Participants { get; set; }
        public virtual List<Prize> Prizes { get; set; }
    }
}
