using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery.Data.Interfaces
{
    interface IAuthorization
    {
        bool Authorize(string login, string password);        
    }
}
