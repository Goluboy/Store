using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Microsoft.EntityFrameworkCore;
using Marketplace.Data;
using Store.Contracts.DTOs;
using Store.Contracts.Requests;

namespace Marketplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MarketplaceDbContext _context;

        public UserController(MarketplaceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(CancellationToken cancellationToken)
        {
            return await _context.Users
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            };
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserRequest userRequest, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            userRequest.UserId = user.UserId;

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserRequest userRequest, CancellationToken cancellationToken)
        {
            if (id != userRequest.UserId)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userRequest.Name;
            user.Email = userRequest.Email;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}