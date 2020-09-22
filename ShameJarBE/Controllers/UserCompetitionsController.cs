using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShameJarBE.Models;

namespace ShameJarBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserCompetitionsController : ControllerBase
    {
        private readonly DataContext _context;

        public UserCompetitionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<UserCompetition> Get()
        {
            return _context.UserCompetition;
        }
    }
}