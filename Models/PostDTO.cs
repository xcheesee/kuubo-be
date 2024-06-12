namespace koob_be.Models;
public class PostDTO
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public int UserId { get; set; }
    public UserDTO? User { get; set; } = null!;
    public int Likes { get; set; }
    public int? CommentCount { get; set; }
    public bool? Liked { get; set; }
}
