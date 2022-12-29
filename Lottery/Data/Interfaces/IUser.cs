using Lottery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Interfaces
{
    public interface IUser
    {
        void SetUser(User user);

        User GetUser(int id);

        User GetUser(string mail);

        User GetUserByKey(string key);

        bool CheckUser(string mail);

        bool CheckKey(string key);

        IEnumerable<User> Users { get; }
    }
}
