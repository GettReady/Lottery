using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lottery.Data.Interfaces;
using Lottery.ViewModels;
using Lottery.Data.Models;


namespace Lottery.Controllers
{
    public class ApiController : Controller
    {

        private readonly IRaffle Raffle;
        private readonly IUser User;
        private readonly IPrize Prize;
        private readonly IUserRaffle UserRaffle;
        static private readonly RafflesUserViewModel RafflesUser = new RafflesUserViewModel();
        static private readonly RafflePrizesViewModel RafflePrizes = new RafflePrizesViewModel();

        public ApiController(IRaffle raffle, IUser user, IPrize prize, IUserRaffle userRaffle)
        {
            Raffle = raffle;
            User = user;
            Prize = prize;
            UserRaffle = userRaffle;
        }

        [HttpPost]
        public string SetUpRaffle(Raffle raffle, List<Prize> prizes, string userKey)
        {
            if (User.CheckKey(userKey))
            {
                string apiKey = Randomizer.GenerateApiKey();

                raffle.Key = apiKey;
                raffle.IsPrivate = true;
                raffle.AuthorId = User.GetUserByKey(userKey).Id;
                raffle.Places = prizes.Count();
                if (raffle.ExpirationTime < DateTime.Now)
                {
                    raffle.Expired = true;
                }
                else
                {
                    raffle.Expired = false;
                }
                Raffle.SetRaffle(raffle);

                UserRaffle.SetAuthor(raffle.Id, raffle.AuthorId);

                foreach (Prize prize in prizes)
                {
                    prize.RaffleId = raffle.Id;
                }
                Prize.SetPrizes(prizes);

                return apiKey;
            }

            return "";
            return "Ошибка: неправльный ключ пользователя.";
        }
        
        [HttpPost]
        public void AddParticipants(string raffleKey, string participants, char devider, bool duplicateCheck=true)
        {            
            if (participants != null)
            {
                if (duplicateCheck)
                {
                    Raffle.AddParticipants(raffleKey, participants, devider, 1);
                }
                else
                {
                    Raffle.AddParticipants(raffleKey, participants, devider, 0);
                }                    
            }
        }
    }
}
