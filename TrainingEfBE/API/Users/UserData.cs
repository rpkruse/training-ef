using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Users
{
    /// <summary>
    /// UserData is used to get the actual data from the context (IE the database)
    /// </summary>
    public class UserData : IUserData
    {
        private readonly DataContext _context;

        public UserData(DataContext context) 
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.User.Include(p => p.Posts).ToList();
        }

        public User GetUser(int userID)
        {
            return _context.User.AsNoTracking().SingleOrDefault(u => u.UserID == userID);
        }

        public User GetUser(string username)
        {
            return _context.User.AsNoTracking().SingleOrDefault(u => u.Username == username);
        }

        public User AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User UpdateUser(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();

            return user;
        }

        public bool DeleteUser(int userID)
        {
            User user = GetUser(userID);

            if (user == null)
            {
                throw new Exception($"Unable to find user to delete with user id {userID}");
            }

            _context.User.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public List<User> GetUsersByRoomID(int RoomID)
        {
            var userRoomList = _context.UserRoom.AsNoTracking().Where(u => u.RoomID == RoomID).ToList();
            var userList = new List<User>();

            //userList.AddRange(_context.Room.Where(r => r.RoomID == RoomID)
            //    .Join(_context.UserRoom
            //    , ur => ur.RoomID
            //    , (ur) => new { ur })
            //    .Join(_context.User
            //    , u => new { u })
            //    );

            foreach (UserRoom u in userRoomList)
            {
                userList.Add(this.GetUser(u.UserID));
            }

            return userList;
        }
    }
}
