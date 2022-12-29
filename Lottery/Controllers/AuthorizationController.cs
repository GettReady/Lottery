using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data.Interfaces;
using Lottery.Data.Models;
using Lottery.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUser User;
        private readonly IRaffle Raffle;
        static private readonly RafflesUserViewModel RafflesUser = new RafflesUserViewModel();

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
            if (user != null)
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

        public void SetRafflesUser(IEnumerable<Raffle> raffles)
        {

        }

    }
}
