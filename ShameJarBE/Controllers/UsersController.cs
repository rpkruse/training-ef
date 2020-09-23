using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShameJarBE.Models;

namespace ShameJarBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.User;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _context.User.AsNoTracking().SingleOrDefaultAsync(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]


        // TODO: https://stackoverflow.com/questions/39802164/asp-net-mvc-how-to-hash-password
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            user.Password = Crypto.HashPassword(user.Password);

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }
    }
}
