using Microsoft.AspNetCore.Mvc;
using Lottery.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.ViewModels;
using Lottery.Data.Models;

namespace Lottery.Controllers
{
    public class RaffleListController : Controller
    {
        private readonly IRaffle Raffle;
        private readonly IUser User;
        private readonly IPrize Prize;
        private readonly IUserRaffle UserRaffle;
        static private readonly RafflesUserViewModel RafflesUser = new RafflesUserViewModel();
        static private readonly RafflePrizesViewModel RafflePrizes = new RafflePrizesViewModel();

        public RaffleListController(IRaffle raffle, IUser user, IPrize prize, IUserRaffle userRaffle)
        {
            Raffle = raffle;
            User = user;
            Prize = prize;
            UserRaffle = userRaffle;
        }
        
        public ViewResult ShowRaffles()
        {
            SetRafflesUser(Raffle.AllRaffles);
            ViewBag.Title = "Все розыгрыши";
            return View(RafflesUser);
        }

        
        public ViewResult ShowRafflePage(int id)
        {
            HttpContext.Session.SetInt32("raffleId", id);
            Raffle raffle = Raffle.GetRaffle(id);
            SetRafflesUser(new List<Raffle>() { raffle });
            ViewBag.Title = raffle.Title;
            return View(RafflesUser);
        }

        public ViewResult ShowActiveRaffles()
        {
            SetRafflesUser(Raffle.ActiveRaffles);
            ViewBag.Title = "Активные розыгрыши";
            return View("ShowRaffles", RafflesUser);
        }

        public ViewResult ShowUserRaffles()
        {
            SetRafflesUser(Raffle.GetUserRaffles((int)HttpContext.Session.GetInt32("userId")));
            ViewBag.Title = "Мои розыгрыши";
            return View("ShowRaffles", RafflesUser);
        }

        [HttpGet]
        public ViewResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Authorization(string userEmail)
        {
            //Авторизация здесь

            var user = User.GetUser(userEmail);
            if(user!=null)
            {
                RafflesUser.User = user;
                HttpContext.Session.SetInt32("userId", RafflesUser.User.Id);
                HttpContext.Session.SetString("userEmail", RafflesUser.User.Email);

                SetRafflesUser(Raffle.AllRaffles);
                ViewBag.Title = "Все розыгрыши";
                return View("ShowRaffles", RafflesUser);
            }
            RafflesUser.User = null;
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userEmail");

            return View();
        }

        public ViewResult CreateRaffle()
        {
            return View("EditRaffle");
        }

        [HttpPost]
        public ViewResult CreateRaffle(RafflePrizesViewModel rp)
        {
            rp.Raffle.Places = 3;
            rp.Raffle.ExpirationTime = rp.Raffle.ExpirationTime.Add(rp.Time.TimeOfDay);
            if (rp.Raffle.ExpirationTime < DateTime.Now)
            {
                rp.Raffle.Expired = true;
            }
            else
            {
                rp.Raffle.Expired = false;
            }
            rp.Raffle.AuthorId = (int)HttpContext.Session.GetInt32("userId");            
            if(rp.Raffle.IsPrivate)
            {
                rp.Raffle.Key = Randomizer.GenerateApiKey();
            }
            Raffle.SetRaffle(rp.Raffle);

            UserRaffle.SetAuthor(rp.Raffle.Id, (int)HttpContext.Session.GetInt32("userId"));
            foreach(Prize prize in rp.Prizes)
            {
                prize.RaffleId = rp.Raffle.Id;
            }
            Prize.SetPrizes(rp.Prizes);
            SetRafflesUser(new List<Raffle>() { rp.Raffle });

            HttpContext.Session.SetInt32("raffleId", RafflesUser.Raffles.First().Id);
            return View("ShowRafflePage", RafflesUser);
        }

        public ViewResult EditRaffle()
        {
            int id = (int)HttpContext.Session.GetInt32("raffleId");

            RafflePrizes.Raffle = Raffle.GetRaffle(id);
            RafflePrizes.Prizes = Prize.GetPrizes(id).ToList();
            RafflePrizes.Time = new DateTime().AddHours(RafflePrizes.Raffle.ExpirationTime.Hour).AddMinutes(RafflePrizes.Raffle.ExpirationTime.Minute);
            return View(RafflePrizes);
        }

        [HttpPost]
        public ViewResult EditRaffle(RafflePrizesViewModel rp)
        {
            rp.Raffle.Id = (int)HttpContext.Session.GetInt32("raffleId");
            rp.Raffle.ExpirationTime = rp.Raffle.ExpirationTime.Add(rp.Time.TimeOfDay);
            if (rp.Raffle.ExpirationTime < DateTime.Now)
            {
                rp.Raffle.Expired = true;
            }
            else
            {
                rp.Raffle.Expired = false;
            }
            Raffle.EditRaffle(rp.Raffle);
            Prize.EditPrizes((int)HttpContext.Session.GetInt32("raffleId"), rp.Prizes);
            SetRafflesUser(new List<Raffle>() { rp.Raffle });
            return View("ShowRafflePage", RafflesUser);
        }

        public ViewResult EditRaffleTile(int id)
        {
            HttpContext.Session.SetInt32("raffleId", id);
            RafflePrizes.Raffle = Raffle.GetRaffle(id);
            RafflePrizes.Prizes = Prize.GetPrizes(id).ToList();
            RafflePrizes.Time = new DateTime().AddHours(RafflePrizes.Raffle.ExpirationTime.Hour).AddMinutes(RafflePrizes.Raffle.ExpirationTime.Minute);
            return View("EditRaffle", RafflePrizes);
        }

        public ViewResult DeleteRaffle()
        {
            Raffle.DeleteRaffle((int)HttpContext.Session.GetInt32("raffleId"));
            SetRafflesUser(Raffle.AllRaffles);
            return View("ShowRaffles", RafflesUser);
        }

        public ViewResult DeleteRaffleTile(int id)
        {
            Raffle.DeleteRaffle(id);
            SetRafflesUser(Raffle.AllRaffles);
            return View("ShowRaffles", RafflesUser);
        }

        public ViewResult FinishRaffle()
        {
            Raffle.FinishRaffle((int)HttpContext.Session.GetInt32("raffleId"));
            SetRafflesUser(new List<Raffle>() { Raffle.GetRaffle((int)HttpContext.Session.GetInt32("raffleId")) });
            return View("ShowRafflePage", RafflesUser);
        }

        public void SetWinners()
        {
            
        }

        [HttpPost]
        public void SetParticipant(int userId, int raffleId)
        {
            if (!User.GetUser((int)HttpContext.Session.GetInt32("userId")).Admin)
            {
                UserRaffle.AddParticipant(raffleId, userId);
            }
        }

        [HttpPost]
        public void Participate(int raffleId=0)
        {
            if (raffleId == 0)
            {
                if (!User.GetUser((int)HttpContext.Session.GetInt32("userId")).Admin)
                {
                    UserRaffle.AddParticipant((int)HttpContext.Session.GetInt32("raffleId"), (int)HttpContext.Session.GetInt32("userId"));
                }
            }
            else
            {
                if (!User.GetUser((int)HttpContext.Session.GetInt32("userId")).Admin)
                {
                    UserRaffle.AddParticipant(raffleId, (int)HttpContext.Session.GetInt32("userId"));
                }
            }
        }


        [HttpPost]
        public void SaveParticipants(string participants)
        {
            if (participants != null)
            {
                Raffle.SetParticipants(Raffle.GetRaffle((int)HttpContext.Session.GetInt32("raffleId")).Key, participants, '\n');
            }
            else
                Raffle.SetParticipants(Raffle.GetRaffle((int)HttpContext.Session.GetInt32("raffleId")).Key, "", '\n');
        }

        public void SetRafflesUser(IEnumerable<Raffle> raffles)
        {
            List<Raffle> usrRaffles = new List<Raffle>();
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                RafflesUser.User = User.GetUser((int)HttpContext.Session.GetInt32("userId"));
                usrRaffles = Raffle.GetUserRaffles(RafflesUser.User.Id).ToList();
            }
            if (raffles.Count() == 1)
            {
                if (raffles.First().Expired)
                {
                    if(raffles.First().IsPrivate) //Записываем в модель массив приватных победителей
                    {
                        RafflesUser.PrivateWinners = Raffle.GetPrivateWinners(raffles.First());
                    }
                    else
                    {
                        RafflesUser.Winners = Raffle.GetWinners(raffles.First());
                    }                   
                }
                else
                {
                    RafflesUser.Winners = null;
                }                    
                RafflesUser.Prizes = Prize.GetPrizes(raffles.First().Id);
            }
            else
            {
                RafflesUser.Prizes = Prize.AllPrizes;
            }

            if (usrRaffles.Count != 0)
            {
                RafflesUser.UserRaffles = new int[usrRaffles.Count];
                for (int i = 0; i < usrRaffles.Count; ++i)
                {
                    RafflesUser.UserRaffles[i] = usrRaffles[i].Id;
                }
            }

            RafflesUser.Raffles = raffles;
        }
    }
}
