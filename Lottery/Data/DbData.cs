using Lottery.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data
{
    public class DbData
    {
        public static void Initialize(LotteryDbContext context)
        {
            if (!context.User.Any())
            {
                context.User.AddRange(Users);
            }

            if (!context.Raffle.Any())
            {
                context.Raffle.AddRange(/*Raffles.Select(c => c.Value)*/Raffles);
            }

            if (!context.UserRaffle.Any())
            {
                context.UserRaffle.AddRange(UsersRaffles);
            }

            if (!context.Prize.Any())
            {
                context.Prize.AddRange(Prizes);
            }

            context.SaveChanges();

        }

        private static List<Raffle> raffleList;
        private static List<User> userList;
        private static List<UserRaffle> userraffleList;
        private static List<Prize> prizesList;

        private static string SampleDescription = "В розыгрыше подарков участвуют работники подразделений и организаций ПАО \"КАМАЗ\", " +
            "расположенных в пределах г. Набережные Челны, которые прошли оба этапа иммунизации от COVID-19. До конца сентября 2020 года " +
            "участники будут получать СМС-сообщения с информацией о победителях, которые набрали больше всего баллов на каждом из этапов " +
            "иммунизации, а также их фамилии, имена и отчества, отдел и должность. С 1 по 30 сентября будут объявлены победители.";

        public static List<Raffle> Raffles
        {
            get
            {
                raffleList = new List<Raffle> {                 
                    (new Raffle { Title = "Розыгрыш 1", Description = "25-го июля будет проводится розыгрыш трёх призов!", FullDescription = SampleDescription, Expired = false, Places = 3, ExpirationTime = new DateTime(2021, 7, 25) }),
                    (new Raffle { Title = "Розыгрыш 2", Description = "В июле будет проводится розыгрыш нескольких призов!", FullDescription = SampleDescription, Expired = false, Places = 3, ExpirationTime = new DateTime(2021, 7, 27) }),
                    (new Raffle { Title = "Розыгрыш 3", Description = "17-го июля будет проводится розыгрыш двух призов!", FullDescription = SampleDescription, Expired = false, Places = 2, ExpirationTime = new DateTime(2021, 7, 17) }),
                    (new Raffle { Title = "Розыгрыш 4", Description = "4 приза будут разыграны 12-го июля.", Expired = true, FullDescription = SampleDescription, Places = 4, ExpirationTime = new DateTime(2021, 7, 12) }),
                    (new Raffle { Title = "Розыгрыш 5", Description = "17-го июля будет объявлен победитель.", Expired = false, FullDescription = SampleDescription, Places = 1, ExpirationTime = new DateTime(2021, 7, 17) }),
                    (new Raffle { Title = "Розыгрыш 6", Description = "15 победителей ждут интересные призы!", Expired = true, FullDescription = SampleDescription, Places = 2, ExpirationTime = new DateTime(2021, 7, 20) })
                };
                    
                return raffleList;
            }
        }

        public static List<User> Users
        {
            get
            {
                if (userList == null)
                {
                    var list = new User[]
                    {
                        new User {Email="eroxinand@yandex.ru", Admin=true, Name="Андрей", Surname="Ерохин"},                        
                        new User {Email="mail1@mail.com", Admin=false, Name="Константин", Surname="Самойлов"},
                        new User {Email="mail2@mail.com", Admin=false, Name="Федор", Surname="Грек"},
                        new User {Email="mail3@mail.com", Admin=false, Name="Максим", Surname="Никифоров"},
                        new User {Email="mail4@mail.com", Admin=false, Name="Эльвира", Surname="Симотина"},
                        new User {Email="mail5@mail.com", Admin=false, Name="Алина", Surname="Быстрова"},
                        new User {Email="mail6@mail.com", Admin=false, Name="Дарья", Surname="Закиулина"},
                        new User {Email="mail7@mail.com", Admin=false, Name="Петр", Surname="Иванов"}
                    };

                    userList = new List<User>();
                    foreach (User item in list)
                    {
                        userList.Add(item);
                    }
                }

                return userList;
            }
        }

        public static List<UserRaffle> UsersRaffles
        {
            get
            {
                userraffleList = new List<UserRaffle>();
                userraffleList.AddRange(new List<UserRaffle>
                {
                    new UserRaffle { RaffleId = 1, UserId = 1, Status = "author" },
                    new UserRaffle { RaffleId = 1, UserId = 2, Status = "participant" },
                    new UserRaffle { RaffleId = 1, UserId = 3, Status = "participant" },
                    new UserRaffle { RaffleId = 1, UserId = 4, Status = "participant" },
                    new UserRaffle { RaffleId = 1, UserId = 5, Status = "participant" },
                    new UserRaffle { RaffleId = 1, UserId = 6, Status = "participant" },

                    new UserRaffle { RaffleId = 2, UserId = 1, Status = "author" },
                    new UserRaffle { RaffleId = 2, UserId = 4, Status = "participant" },
                    new UserRaffle { RaffleId = 2, UserId = 2, Status = "participant" },
                    new UserRaffle { RaffleId = 2, UserId = 3, Status = "participant" },
                    new UserRaffle { RaffleId = 2, UserId = 5, Status = "participant" },
                    new UserRaffle { RaffleId = 2, UserId = 7, Status = "participant" },
                    new UserRaffle { RaffleId = 2, UserId = 8, Status = "participant" },
                    new UserRaffle { RaffleId = 2, UserId = 6, Status = "participant" },

                    new UserRaffle { RaffleId = 3, UserId = 1, Status = "author" },
                    new UserRaffle { RaffleId = 3, UserId = 2, Status = "participant" },
                    new UserRaffle { RaffleId = 3, UserId = 8, Status = "participant" },
                    new UserRaffle { RaffleId = 3, UserId = 4, Status = "participant" },
                    new UserRaffle { RaffleId = 3, UserId = 6, Status = "participant" },

                    new UserRaffle { RaffleId = 4, UserId = 1, Status = "author" },
                    new UserRaffle { RaffleId = 4, UserId = 2, Status = "winner" },
                    new UserRaffle { RaffleId = 4, UserId = 3, Status = "participant" },
                    new UserRaffle { RaffleId = 4, UserId = 4, Status = "winner" },
                    new UserRaffle { RaffleId = 4, UserId = 8, Status = "winner" },
                    new UserRaffle { RaffleId = 4, UserId = 7, Status = "participant" },
                    new UserRaffle { RaffleId = 4, UserId = 6, Status = "winner" },
                    new UserRaffle { RaffleId = 4, UserId = 5, Status = "participant" },

                    new UserRaffle { RaffleId = 5, UserId = 1, Status = "author" },
                    new UserRaffle { RaffleId = 5, UserId = 2, Status = "participant" },
                    new UserRaffle { RaffleId = 5, UserId = 6, Status = "participant" },
                    new UserRaffle { RaffleId = 5, UserId = 5, Status = "participant" },
                    new UserRaffle { RaffleId = 5, UserId = 4, Status = "participant" },
                    new UserRaffle { RaffleId = 5, UserId = 8, Status = "participant" },

                    new UserRaffle { RaffleId = 6, UserId = 1, Status = "author" },
                    new UserRaffle { RaffleId = 6, UserId = 2, Status = "participant" },
                    new UserRaffle { RaffleId = 6, UserId = 3, Status = "participant" },
                    new UserRaffle { RaffleId = 6, UserId = 7, Status = "winner" },
                    new UserRaffle { RaffleId = 6, UserId = 5, Status = "winner" }

                });

                return userraffleList;
            }
        }

        public static List<Prize> Prizes
        {
            get
            {
                prizesList = new List<Prize>();
                string[] prizes = new string[] { "Автомобиль", "100000 руб.", "Шапка", "Отпуск", "Шоколадка", "Большая шоколадка"};
                var rand = new Random();

                prizesList.AddRange(new List<Prize>
                {
                    new Prize {RaffleId=1, Place=1, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=1, Place=2, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=1, Place=3, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=2, Place=1, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=2, Place=2, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=2, Place=3, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=3, Place=1, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=3, Place=2, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=4, Place=1, PrizeDescription=prizes[rand.Next(0, 6)], WinnerId=8},
                    new Prize {RaffleId=4, Place=2, PrizeDescription=prizes[rand.Next(0, 6)], WinnerId=6},
                    new Prize {RaffleId=4, Place=3, PrizeDescription=prizes[rand.Next(0, 6)], WinnerId=4},
                    new Prize {RaffleId=4, Place=4, PrizeDescription=prizes[rand.Next(0, 6)], WinnerId=2},
                    new Prize {RaffleId=5, Place=1, PrizeDescription=prizes[rand.Next(0, 6)]},
                    new Prize {RaffleId=6, Place=1, PrizeDescription=prizes[rand.Next(0, 6)], WinnerId=5},
                    new Prize {RaffleId=6, Place=2, PrizeDescription=prizes[rand.Next(0, 6)], WinnerId=7}

                });

                return prizesList;
            }
        }

        static List<Prize> Prize(LotteryDbContext context)
        {
            prizesList = new List<Prize>();
            string[] prizes = new string[] { "Автомобиль", "100000 руб.", "Шапка", "Отпуск", "Шоколадка", "Большая шоколадка" };
            var rand = new Random();

            List<UserRaffle> allWinners = userraffleList.Where(c => c.Status == "winner").ToList();

            foreach (Raffle raffle in context.Raffle)
            {
                if (raffle.Expired)
                {
                    List<UserRaffle> winners = allWinners.Where(c => c.RaffleId == raffle.Id).ToList();
                    for (int i = 0; i < raffle.Places; ++i)
                    {
                        prizesList.Add(new Prize { RaffleId = raffle.Id, Place = i + 1, WinnerId = winners[i].UserId, PrizeDescription = prizes[rand.Next(0, 6)] });
                    }
                }
                else
                {
                    for (int i = 0; i < raffle.Places; ++i)
                    {
                        prizesList.Add(new Prize { RaffleId = raffle.Id, Place = i + 1, PrizeDescription = prizes[rand.Next(0, 6)] });
                    }
                }
            }

            return prizesList;
        }


        static List<UserRaffle> UserRaffle(LotteryDbContext context)
        {
            userraffleList = new List<UserRaffle>();
            var rand = new Random();

            foreach (Raffle raffle in context.Raffle)
            {
                userraffleList.Add(new UserRaffle { RaffleId = raffle.Id, UserId = context.User.First().Id, Status = "author" });

                if (raffle.Expired)
                {
                    List<int> ids = context.User.Select(c => c.Id).ToList();

                    for (int i = 0; i < raffle.Places; ++i)
                    {
                        int winnerId = rand.Next(0, ids.Count());
                        Console.WriteLine(ids.Count());
                        userraffleList.Add(new UserRaffle { RaffleId = raffle.Id, UserId = ids[winnerId], Status = "winner" });
                        ids.RemoveAt(winnerId);                        
                    }
                    for (int i = 0; i < rand.Next(0, ids.Count()); ++i)
                    {
                        int winnerId = rand.Next(0, ids.Count());
                        userraffleList.Add(new UserRaffle { RaffleId = raffle.Id, UserId = ids[winnerId], Status = "participant" });
                        ids.RemoveAt(winnerId);
                    }
                }
                else
                {
                    List<int> ids = context.User.Select(c => c.Id).ToList();
                    int participants = raffle.Places + rand.Next(0, context.User.Count() - raffle.Places);
                    for (int i = 0; i < participants; ++i)
                    {
                        int winnerId = rand.Next(0, ids.Count());
                        userraffleList.Add(new UserRaffle { RaffleId = raffle.Id, UserId = ids[winnerId], Status = "participant" });
                        ids.RemoveAt(winnerId);
                    }
                }
            }

            return userraffleList;
        }
    }
}
