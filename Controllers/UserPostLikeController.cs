using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using koob_be.Models;

namespace koob_be.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserPostLikeController(ApplicationContext context) : ControllerBase
{
    private readonly ApplicationContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserPostLike>>> GetUserPostLikes()
    {
        return await _context.UserPostLikes
        .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserPostLike>> GetUserPostLikesById(int id)
    {
        var userLike = await _context.UserPostLikes
        .FirstOrDefaultAsync(pl => pl.Id == id);

        if (userLike == null)
        {
            return NotFound();
        }

        return userLike;


    }

    [HttpPost]
    public async Task<ActionResult<UserPostLike>> PostUserPostLikes(UserPostLikeDTO like)
    {
        var userLike = new UserPostLike
        {
            UserId = like.UserId,
            PostId = like.PostId,
        };

        var currLike = await _context.UserPostLikes
        .FirstOrDefaultAsync(pl => (pl.PostId == userLike.PostId) && (pl.UserId == userLike.UserId));

        if (currLike != null)
        {
            return BadRequest();
        }
        _context.UserPostLikes.Add(userLike);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUserPostLikesById), new { userLike.Id }, userLike);
    }

    [HttpDelete("{UserId}/{PostId}")]
    public async Task<ActionResult> DeleteUserPostLike(int UserId, int PostId)
    {
        var like = await _context.UserPostLikes
        .FirstOrDefaultAsync(pl => pl.UserId == UserId && pl.PostId == PostId);

        if (like == null)
        {
            return NotFound();
        }

        _context.UserPostLikes.Remove(like);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
