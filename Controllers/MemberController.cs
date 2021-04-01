using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using testAngular.Data;
using testAngular.Models;
using testAngular.Services;

namespace testAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: api/<DataController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> MemberList()
        {
            return await _context.Members.ToListAsync();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<Member>>  GetMember(long id)
        {
            var model = await _context.Members.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(long id, Member model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Update(model);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Member>> InsertMember([FromBody] Member model)
        {
            var identity = User.Identity as ClaimsIdentity;
            Claim identityClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            model.ApplicationUserId = _context.Users.FirstOrDefault(u => u.Id == identityClaim.Value).Id;
            _context.Members.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMember", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Member>> DeleteMember(long id)
        {
            var model = await _context.Members.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            _context.Members.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
