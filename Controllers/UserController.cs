using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using koob_be.Models;

namespace koob_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ApplicationContext context) : ControllerBase
    {
        private readonly ApplicationContext _context = context;

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context
            .Users
            .Select(user => UserToDTO(user))
            .ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserToDTO(user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Pw = "senhaSeguraConfia" + user.Pw,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = newUser.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private static UserDTO UserToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
