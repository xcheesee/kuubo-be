namespace koob_be.Models;

public class UserPostLikeDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int PostId { get; set; }
    public Post? Post { get; set; }
};
