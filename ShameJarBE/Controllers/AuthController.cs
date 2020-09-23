using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShameJarBE.Models;

namespace ShameJarBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private AuthSettings _authSettings;

        public AuthController(DataContext context, AuthSettings authSettings)
        {
            _context = context;
            _authSettings = authSettings;
        }

        private string CreateToken(User user)
        {
            string secret = _authSettings.SECRET; // Dev
                                                  // string secret = Environment.GetEnvironmentVariable("SECRET"); //PROD

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()) };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private OkObjectResult AuthResult(User user)
        {
            string token = CreateToken(user);

            return Ok(
                new
                {
                    Username = user.Username,
                    Token = token
                });
        }

        [Authorize]
        [HttpGet("authUser")]
        public IActionResult Get()
        {
            string id = HttpContext.User.Claims.First().Value;
            User _user = _context.User.SingleOrDefault(u => u.UserID.ToString() == id);
            _user.Password = null;

            return Ok(_user);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUser user) // From https://stackoverflow.com/questions/39802164/asp-net-mvc-how-to-hash-password
        {
            string username = user.Username;
            string password = user.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Unauthorized();
            }

            User _user = _context.User.SingleOrDefault(x => x.Username == username);

            if (_user == null) return Unauthorized();

            bool passMatches = Crypto.VerifyHashedPassword(_user.Password, password);

            if (!passMatches) return Unauthorized();

            return AuthResult(_user);
        }


        // TODO: Move to model object!!!
        public class LoginUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
