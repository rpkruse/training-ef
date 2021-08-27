using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingEfBE.Models;

namespace TrainingEfBE.API.Rooms
{
    public interface IRoomData
    {
        List<Room> GetRooms();

        List<Room> GetRoomsByUserID(int UserID);

        Room AddRoom(NewRoom room);

        bool DeleteRoom(Room room);

        Room GetRoomByRoomID(int roomID);
        void AddUserToRoom(UserRoom RoomUser);
        Room GetRoomByRoomName(string roomName);
        bool RemoveUserFromRoom(UserRoom RoomUser);

        bool UpdateRoom(Room UpdatedRoom);


        UserRoom GetUserRoom(UserRoom accessor);
    }
}
