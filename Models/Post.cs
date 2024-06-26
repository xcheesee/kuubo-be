using System.ComponentModel.DataAnnotations.Schema;

namespace koob_be.Models;

public class Post
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Content { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
}
