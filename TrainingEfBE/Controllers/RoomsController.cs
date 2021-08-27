using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrainingEfBE.API.Posts;
using TrainingEfBE.API.Rooms;
using TrainingEfBE.API.Users;
using TrainingEfBE.Models;

namespace TrainingEfBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController: ControllerBase
    {


        private readonly RoomAPI _roomAPI;
        private readonly UserAPI _userAPI;

        public RoomsController(DataContext _context)
        {
            _roomAPI = new RoomAPI(new RoomData(_context));
            _userAPI = new UserAPI(new UserData(_context));

        }

        [HttpGet]
        public List<Room> Get()
        {
            return _roomAPI.GetRooms();
        }

        [HttpGet("byRID/{roomID}")]
        public Room GetRoomByRoomID([FromRoute] int roomID)
        {
            return _roomAPI.GetRoomByRoomID(roomID);
        }

        [HttpGet("byUID/{userID}")]
        public List<Room> GetRoomsByUserID([FromRoute] int userID)
        {
            return _roomAPI.GetRoomsByUserID(userID);
        }

        [HttpGet("byRoomName/{RoomName}")]
        public Room GetRoomByRoomName([FromRoute] string RoomName)
        {
            return _roomAPI.GetRoomByRoomName(RoomName);
        }

        [HttpPost]
        public IActionResult PostRoom([FromBody] NewRoom roomToAdd)
        {
            Room _roomName = _roomAPI.GetRoomByRoomName(roomToAdd.RoomName);

            if (_roomName != null)
            {
                ModelState.AddModelError("Error", "Room name already taken");
                return BadRequest(ModelState);
            }
            roomToAdd.Password = Crypto.HashPassword(roomToAdd.Password);

            Room _room = _roomAPI.AddRoom(roomToAdd);

            if (_room == null)
            {
                ModelState.AddModelError("Error", "Invalid new room payload");
                return BadRequest(ModelState);
            }

            return CreatedAtAction("GetRoomByRoomID", new { roomID = _room.RoomID }, _room);
        }

        [HttpPost("CanAccessRoom")]
        public IActionResult CheckAuthorizationToRoom([FromBody] UserRoom accessor)
        {
            if (!_roomAPI.GetUserAccess(accessor))
            {
                
                return Ok(false);
            }

            return Ok(true);
        }



        [HttpPost("AddUserRoom")]
        public IActionResult AddUserToRoom([FromBody] UserRoom RoomUser)
        {
            User _user = _userAPI.GetUser(RoomUser.UserID);

            if (_user == null)
            {
                ModelState.AddModelError("Error", "User not found, cannot be added to room");
                return BadRequest(ModelState);
            }

            Room _room = _roomAPI.GetRoomByRoomID(RoomUser.RoomID);

            if (_room == null)
            {
                ModelState.AddModelError("Error", "Room does not exist");
                return BadRequest(ModelState);
            }

            // We can go over password hashing later
            //user.Password = Crypto.HashPassword(user.Password);
            try{
                var roomsUserIsIn = _roomAPI.GetRoomsByUserID(RoomUser.UserID);
                if (roomsUserIsIn.FindIndex(u => u.RoomID == _room.RoomID) > -1) {
                    ModelState.AddModelError("Error", "You are already in this room");
                    return BadRequest(ModelState);
                }
                _roomAPI.AddUserToRoom(RoomUser);

            }
            catch(Exception e)
            {
                ModelState.AddModelError("Error", $"Failed to add user to room, {e.Message}: {e.InnerException}");
                return BadRequest(ModelState);

            }
            

            return NoContent();
        }

        [HttpPost("RemoveUserRoom")]
        public IActionResult RemoveUserFromRoom([FromBody] UserRoom RoomUser)
        {
            User _user = _userAPI.GetUser(RoomUser.UserID);

            if (_user == null)
            {
                ModelState.AddModelError("Error", "User not found, cannot be removed");
                return BadRequest(ModelState);
            }

            Room _room = _roomAPI.GetRoomByRoomID(RoomUser.RoomID);

            if (_room == null)
            {
                ModelState.AddModelError("Error", "Room does not exist");
                return BadRequest(ModelState);
            }

            // We can go over password hashing later
            //user.Password = Crypto.HashPassword(user.Password);
            try
            {
                _roomAPI.RemoveUserFromRoom(RoomUser);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", $"Failed to remove user to room, {e.Message}: {e.InnerException}");
                return BadRequest(ModelState);

            }


            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateRoom([FromBody] Room updatedRoom)
        {
            Room _roomID = _roomAPI.GetRoomByRoomID(updatedRoom.RoomID);

            if (_roomID == null)
            {
                ModelState.AddModelError("Error", "Room does not exist");
                return BadRequest(ModelState);
            }

            bool _room = _roomAPI.UpdateRoom(updatedRoom);

            if (!_room)
            {
                ModelState.AddModelError("Error", "Invalid new room payload");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost("RoomLogin")]
        public IActionResult LoginToRoom([FromBody] RoomLogin Login)
        {
            User _user = _userAPI.GetUser(Login.UserID);

            if (_user == null)
            {
                ModelState.AddModelError("Error", "User not found, cannot be added to room");
                return BadRequest(ModelState);
            }

            Room _room = _roomAPI.GetRoomByRoomName(Login.RoomName);

            if (_room == null)
            {
                ModelState.AddModelError("Error", "Room does not exist");
                return BadRequest(ModelState);
            }

            bool passMatches = Crypto.VerifyHashedPassword(_room.Password, Login.Password);

            if (!passMatches)
            {
                ModelState.AddModelError("Error", "Incorrect Passowrd");
                return BadRequest(ModelState);
            }


            // We can go over password hashing later
            //user.Password = Crypto.HashPassword(user.Password);
            try
            {
                UserRoom roomUser = new UserRoom(){ UserID = Login.UserID, RoomID = _room.RoomID };
                var roomsUserIsIn = _roomAPI.GetRoomsByUserID(roomUser.UserID);
                if (roomsUserIsIn.FindIndex(u => u.RoomID == _room.RoomID) > -1)
                {
                    ModelState.AddModelError("Error", "You are already in this room");
                    return BadRequest(ModelState);
                }
                _roomAPI.AddUserToRoom(roomUser);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", $"Failed to add user to room, {e.Message}: {e.InnerException}");
                return BadRequest(ModelState);

            }


            return Ok(_room);
        }

        //[HttpGet("updatePass")]
        //public IActionResult UpdatePasswords()
        //{
        //    var rooms = _roomAPI.GetRooms();
        //    foreach (Room r in rooms)
        //    {
        //        r.Password = Crypto.HashPassword(r.Password);
        //        _roomAPI.UpdateRoom(r);
        //    }
        //    return Ok();
        //}




    }
        
}
