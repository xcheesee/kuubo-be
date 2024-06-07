using koob_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace koob_be.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController(ApplicationContext context) : ControllerBase
{
    private readonly ApplicationContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
    {
        return await _context.Posts
            .Include(post => post.User)
            .Select(post => PostToDTO(post))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDTO>> GetPostById(int id)
    {
        var post = await _context.Posts
            .Include(post => post.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return NotFound();
        }

        return PostToDTO(post);
    }

    [HttpPost]
    public async Task<ActionResult<PostDTO>> PostPost([FromForm] PostDTO post)
    {
        var newPost = new Post
        {
            Content = post.Content,
            UserId = post.UserId,
        };
        _context.Posts.Add(newPost);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPostById), new { newPost.Id }, PostToDTO(newPost));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDTO>> PutPost(int id, PostDTO post)
    {
        var dbPost = await _context.Posts.FindAsync(id);
        if (dbPost == null)
        {
            return NotFound();
        }
        dbPost.Content = post.Content;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static PostDTO PostToDTO(Post post)
    {
        if (post.User == null)
        {
            return new PostDTO
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                Likes = post.Likes,

            };
        }

        return new PostDTO
        {
            Id = post.Id,
            Content = post.Content,
            UserId = post.UserId,
            User = new UserDTO
            {
                UserName = post.User.UserName,
                Email = post.User.Email,
                Name = post.User.Name
            },
            Likes = post.Likes,
        };
    }
}
