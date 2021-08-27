using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrainingEfBE.API.Users;
using TrainingEfBE.Models;

namespace TrainingEfBE.Controllers
{
    /// <summary>
    /// UserController handles talking to the frontend.
    /// An example of a call is: GET: <baseUrl>/users/1
    /// In general the URL for the controller is just <baseURL>/< plural controller name>/_____
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserAPI _userAPI;

        // The DataContext param is auto injected by EF
        public UsersController(DataContext _context)
        {
            _userAPI = new UserAPI(new UserData(_context));
        }

        [HttpGet]
        public List<User> Get()
        {
            return _userAPI.GetUsers();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            User user = _userAPI.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("byRID/{id}")]
        public List<User> GetUsersByRoomID([FromRoute] int id)
        {
            return _userAPI.GetUsersByRoomID(id);
        }


        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            User _user = _userAPI.GetUser(user.Username);

            if (_user != null)
            {
                ModelState.AddModelError("Error", "Username already taken");
                return BadRequest(ModelState);
            }

            // We can go over password hashing later
            user.Password = Crypto.HashPassword(user.Password);


            _user = _userAPI.AddUser(user);

            if (_user == null)
            {
                ModelState.AddModelError("Error", "Invalid user payload");
                return BadRequest(ModelState);
            }

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        [HttpPost("login")]
        public IActionResult AttempLogin([FromBody] User user) {

            User _user = _userAPI.GetUser(user.Username);
            bool passMatches = Crypto.VerifyHashedPassword(_user.Password, user.Password);

            if (_user == null || !passMatches)
            {
                ModelState.AddModelError("Error", "Invalid username/password");
                return BadRequest(ModelState);
            }
            //_user.Password = null;

            return Ok(_user);
        }



            [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            User user = _userAPI.GetUser(id);

            if(user == null)
            {
                return NotFound();
            }

            _userAPI.DeleteUser(id);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            User _user = _userAPI.GetUser(user.UserID);

            if(_user == null)
            {
                ModelState.AddModelError("Error", "User not found");
                return BadRequest(ModelState);
            }

            _userAPI.UpdateUser(user);
            return Ok();

        }
    }
}
