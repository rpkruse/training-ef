using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Rooms
{
    public class RoomData : IRoomData
    {

        private readonly DataContext _context;

        public RoomData(DataContext context)
        {
            _context = context;
        }

        public Room AddRoom(NewRoom roomToAdd)
        {
            var room = new Room
            {
                Bio = roomToAdd.Bio,
                ImageURL = roomToAdd.ImageURL,
                Password = roomToAdd.Password,
                RoomName = roomToAdd.RoomName

            };
            _context.Room.Add(room);
            _context.SaveChanges();
            var _room = _context.Room.SingleOrDefault(r => r.RoomID == room.RoomID);
            var newUserRoom = new UserRoom { RoomID = _room.RoomID, UserID = roomToAdd.UserIDs.First() };

            _context.UserRoom.Add(newUserRoom);
            _context.SaveChanges();

            return _room;

        }

        public bool DeleteRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public Room GetRoomByRoomID(int roomID)
        {
            return _context.Room.AsNoTracking().SingleOrDefault(r => r.RoomID == roomID);
        }

        public List<Room> GetRooms()
        {
            return _context.Room.AsNoTracking().ToList();
        }

        public List<Room> GetRoomsByUserID(int userID)
        {
            List<Room> rooms = new List<Room>();
            //var userRooms = _context.UserRoom.AsNoTracking().Where(u => u.UserID == userID).ToList();          //.AsNoTracking().Where(r => r.userID == roomID).FirstOrDefault();
            //foreach (UserRoom ur in userRooms){
            //    var room = this.GetRoomByRoomID(ur.RoomID);
            //    rooms.Add(room);
            //}
            var rooms2 =
            (from room in _context.Room
             join userRoom in _context.UserRoom on room.RoomID equals userRoom.RoomID
             where userRoom.UserID == userID
             select new Room{ RoomID = room.RoomID, Bio = room.Bio, ImageURL = room.ImageURL, RoomName = room.RoomName }).ToList();

            return rooms2;
        }


        public void AddUserToRoom(UserRoom RoomUser)
        {
            _context.UserRoom.Add(RoomUser);
            _context.SaveChanges();
        }

        public Room GetRoomByRoomName(string roomName)
        {
            return _context.Room.AsNoTracking().SingleOrDefault(r => r.RoomName.ToLower() == roomName.ToLower());
        }
        public bool RemoveUserFromRoom(UserRoom RoomUser)
        {
            var userRoom = _context.UserRoom.SingleOrDefault(ur => ur.RoomID == RoomUser.RoomID && ur.UserID == RoomUser.UserID);
            _context.UserRoom.Remove(userRoom);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateRoom(Room updatedRoom)
        {
            _context.Entry(updatedRoom).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public UserRoom GetUserRoom(UserRoom accessor)
        {
            return _context.UserRoom.SingleOrDefault(r => r.RoomID == accessor.RoomID && r.UserID == accessor.UserID);
        }
    }
}
