using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Models;
using server.DTO;
using server.ViewModels;
using System.Linq;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KanbanContext _context;

        public UsersController(KanbanContext context)
        {
            _context = context;
        }

        // POST /api/Users/token
        [HttpPost("token")]
        [Produces("application/json")]
        public async Task<ActionResult<object>> Token(CreateTokenDTO dto)
        {
            var (user, identity) = (await GetIdentity(dto.Username, dto.Password)).GetValueOrDefault();

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                user_id = user.Id,
            };

            return response;
        }

        private async Task<Nullable<ValueTuple<User, ClaimsIdentity>>> GetIdentity(string username, string password)
        {
            var person = await _context.Users.Where(x => x.Login == username && x.Password == password).FirstOrDefaultAsync();
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "Token");

                return (person, identity);
            }

            // если пользователя не найдено
            return null;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            return await _context.Users
                .Select(o => new UserViewModel { Id = o.Id, Email = o.Email, Login = o.Login })
                .ToListAsync();
        }

        // GET: api/Users/:id
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserViewModel { Id = user.Id, Email = user.Email, Login = user.Login };
        }

        // PUT api/Users/:id
        [HttpPatch("{id}")]
        [Authorize]
        [Produces("application/json")]
        public async Task<IActionResult> PatchUser(int id, [FromBody] PatchUserDTO dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = dto.IsFieldPresent(nameof(user.Email)) ? dto.Email : user.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Users/register
        [HttpPost("register")]
        [Produces("application/json")]
        public async Task<ActionResult<UserViewModel>> PostUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }


        // DELETE: api/Users/:id
        [HttpDelete("{id}")]
        [Authorize]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
