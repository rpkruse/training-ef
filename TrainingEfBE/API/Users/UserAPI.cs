using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Users
{
    /// <summary>
    /// UserAPI is how the controller talks to the UserData. It will also do all of the heavy lifting
    /// for data/logic manipulation. Right now it looks like it isn't doing much, but that's b/c our methods
    /// are super simple
    /// </summary>
    public class UserAPI
    {
        private readonly IUserData _userData;

        public UserAPI(IUserData userData)
        {
            _userData = userData;
        }

        public List<User> GetUsers()
        {
            return _userData.GetUsers();
        }

        public User GetUser(int userID)
        {
            return _userData.GetUser(userID);
        }

        public User GetUser(string username)
        {
            return _userData.GetUser(username);
        }

        public User AddUser(User user)
        {
            return _userData.AddUser(user);
        }

        public User UpdateUser(User user)
        {
            return _userData.UpdateUser(user);
        }

        public bool DeleteUser(int userID)
        {
            return _userData.DeleteUser(userID);
        }
    }
}
