using System.Text.Json.Serialization;

namespace koob_be.Models;

public class PostCommentDTO
{
    public int Id { get; set; }
    public required string Comment { get; set; }
    public UserDTO? User { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int Likes { get; set; }

}