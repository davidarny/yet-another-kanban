using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.ViewModels;
using server.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly KanbanContext _context;

        public DesksController(KanbanContext context)
        {
            _context = context;
        }

        // GET: api/desks
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DeskViewModel>>> GetDesks()
        {
            var desks = await _context.Desks.ToListAsync();
            var deskIds = desks.Select(o => o.Id).ToArray();

            var deskUsers = await _context.UserHasDesks
                .Include(o => o.User)
                .Where(o => deskIds.Contains(o.IdDesk))
                .ToListAsync();

            var views = new HashSet<DeskViewModel>();

            foreach (var desk in desks)
            {
                var users = deskUsers
                    .Where(o => o.IdDesk == desk.Id)
                    .Select(o => new UserViewModel { Id = o.User.Id, Email = o.User.Email, Login = o.User.Login })
                    .ToHashSet();

                views.Add(new DeskViewModel { Id = desk.Id, Title = desk.Title, Description = desk.Description, Users = users });
            }

            return views;
        }

        // GET: api/desks/:id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DeskViewModel>> GetDesk(int id)
        {
            var desk = await _context.Desks.FindAsync(id);

            if (desk == null)
            {
                return NotFound();
            }

            var users = await _context.UserHasDesks
                .Include(o => o.User)
                .Where(o => desk.Id == o.IdDesk)
                .Select(o => new UserViewModel { Id = o.User.Id, Email = o.User.Email, Login = o.User.Login })
                .ToListAsync();

            var view = new DeskViewModel { Id = desk.Id, Title = desk.Title, Description = desk.Description, Users = users };

            return view;
        }

        // PATCH: api/desks/:id
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> PatchDesk(int id, [FromBody] PatchDeskDTO dto)
        {
            var desk = await _context.Desks.FindAsync(id);

            if (desk == null)
            {
                return NotFound();
            }

            desk.Title = dto.IsFieldPresent(nameof(desk.Title)) ? dto.Title : desk.Title;
            desk.Description = dto.IsFieldPresent(nameof(desk.Description)) ? dto.Description : desk.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/desks
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DeskViewModel>> PostDesk([FromBody] PostDeskDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var desk = _context.Desks.Add(new Desk { Title = dto.Title, Description = dto.Description }).Entity;
            _context.UserHasDesks.Add(new UserHasDesk { Desk = desk, User = user });

            await _context.SaveChangesAsync();

            var userView = new UserViewModel { Id = user.Id, Email = user.Email, Login = user.Login };

            var deskView = new DeskViewModel {
                Title = desk.Title,
                Description = desk.Description,
                Users = new HashSet<UserViewModel> { userView }
            };

            return CreatedAtAction(nameof(GetDesk), new { id = desk.Id }, deskView);
        }

        // DELETE: api/desks/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDesk(int id)
        {
            var desk = await _context.Desks.FindAsync(id);
            if (desk == null)
            {
                return NotFound();
            }

            _context.Desks.Remove(desk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeskExists(int id)
        {
            return _context.Desks.Any(e => e.Id == id);
        }
    }
}
