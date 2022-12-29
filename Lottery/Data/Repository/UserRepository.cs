using Lottery.Data.Interfaces;
using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Repository
{
    public class UserRepository : IUser
    {
        private LotteryDbContext dbContext;

        public UserRepository(LotteryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SetUser(User user)
        {
            //user.Id = 0;
            user.Key = Randomizer.GenerateApiKey();
            dbContext.User.Add(user);
            dbContext.SaveChanges();
        }

        public User GetUser(int id) => dbContext.User.FirstOrDefault(c => c.Id == id);

        public User GetUser(string mail) => dbContext.User.FirstOrDefault(c => c.Email == mail);

        public User GetUserByKey(string key) => dbContext.User.FirstOrDefault(c => c.Key == key);

        //Проверка существования пользователя в базе
        public bool CheckUser(string mail)
        {
            if (dbContext.User.FirstOrDefault(c => c.Email == mail) != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckKey(string key)
        {
            if (dbContext.User.FirstOrDefault(c => c.Key == key) != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<User> Users => dbContext.User;
    }
}
