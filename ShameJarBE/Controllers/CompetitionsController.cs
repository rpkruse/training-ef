using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShameJarBE.Models;

namespace ShameJarBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitionsController : ControllerBase
    {
        private readonly DataContext _context;

        public CompetitionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Competition> Get()
        {
            return _context.Competition;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompetition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Competition comp = await _context.Competition.AsNoTracking().SingleOrDefaultAsync(comp => comp.CompetitionID == id);

            if (comp == null)
            {
                return NotFound();
            }

            return Ok(User);
        }
    }
}
