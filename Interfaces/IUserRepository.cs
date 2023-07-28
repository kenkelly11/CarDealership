using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersByRole(string Role);
        User GetUserById(string UserId);
        User GetUserByUserName(string UserName);
        void Insert(User user);
        void Update(User user);
    }
}
