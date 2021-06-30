using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Users
{
    public interface IUserData
    {
        List<User> GetUsers();
        User GetUser(int userID);
        User GetUser(string username);
        User AddUser(User user);
        User UpdateUser(User user);
        bool DeleteUser(int userID);
    }
}
