using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Rooms
{
    public class RoomAPI
    {
        private readonly IRoomData _RoomData;

        public RoomAPI(IRoomData roomData)
        {
            _RoomData = roomData;
        }

        public List<Room> GetRooms()
        {
            return _RoomData.GetRooms();
        }

        public Room GetRoomByRoomID(int roomID)
        {
            return _RoomData.GetRoomByRoomID(roomID);
        }

        public Room GetRoomByRoomName(string roomName)
        {
            return _RoomData.GetRoomByRoomName(roomName);
        }

        public List<Room> GetRoomsByUserID(int userID)
        {
            return _RoomData.GetRoomsByUserID(userID);
        }

        public Room AddRoom(NewRoom RoomToAdd)
        {
            return _RoomData.AddRoom(RoomToAdd);
        }
        public void AddUserToRoom(UserRoom RoomUser)
        {
            _RoomData.AddUserToRoom(RoomUser);
        }

        public bool RemoveUserFromRoom(UserRoom RoomUser)
        {
            return _RoomData.RemoveUserFromRoom(RoomUser);
        }

        public bool UpdateRoom(Room UpdatedRoom)
        {
            _RoomData.UpdateRoom(UpdatedRoom);
            return true;
        }

        public bool GetUserAccess(UserRoom accessor)
        {
            return _RoomData.GetUserRoom(accessor) != null;
        }

    }
}
