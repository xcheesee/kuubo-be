using koob_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace koob_be.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostCommentController(ApplicationContext context) : ControllerBase
{
    private readonly ApplicationContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostCommentDTO>>> GetPostComments()
    {
        return await _context.PostComments
        .Include(postComment => postComment.User)
        .Select(comment => PostCommentToDTO(comment))
        .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostCommentDTO>> GetPostComment(int id)
    {
        var postComment = await _context.PostComments
        .Include(postComment => postComment.User)
        .FirstOrDefaultAsync(comment => comment.Id == id);

        if (postComment == null)
        {
            return NotFound();
        }
        return PostCommentToDTO(postComment);
    }

    [HttpPost]
    public async Task<ActionResult<PostComment>> PostPostComment(PostCommentDTO postComment)
    {
        var newComment = new PostComment
        {
            Id = postComment.Id,
            Comment = postComment.Comment,
            UserId = postComment.UserId,
            PostId = postComment.PostId,
        };
        _context.PostComments.Add(newComment);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPostComment", new { id = postComment.Id }, postComment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutPostComment(int id, PostCommentDTO postComment)
    {
        if (id != postComment.Id)
        {
            return BadRequest();
        }

        var changedComment = new PostComment
        {
            Id = postComment.Id,
            Comment = postComment.Comment,
            PostId = postComment.PostId,
            UserId = postComment.UserId,
            Likes = postComment.Likes,
        };

        _context.Entry(changedComment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostCommentExists(id))
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePostComment(int id)
    {
        var postComment = await _context.PostComments.FindAsync(id);

        if (postComment == null)
        {
            return NotFound();
        }

        _context.PostComments.Remove(postComment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PostCommentExists(int id)
    {
        return _context.PostComments.Any(comment => comment.Id == id);
    }

    static private PostCommentDTO PostCommentToDTO(PostComment postComment)
    {
        if (postComment.User == null)
        {
            return new PostCommentDTO
            {
                Id = postComment.Id,
                UserId = postComment.UserId,
                Comment = postComment.Comment,
                Likes = postComment.Likes,
                PostId = postComment.PostId,
            };
        }

        return new PostCommentDTO
        {
            Id = postComment.Id,
            User = new UserDTO
            {
                UserName = postComment.User.UserName,
                Email = postComment.User.Email,
                Name = postComment.User.Name,
            },
            UserId = postComment.UserId,
            Comment = postComment.Comment,
            Likes = postComment.Likes,
            PostId = postComment.PostId,
        };
    }
}