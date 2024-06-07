using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace koob_be.Models;

public class PostComment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Comment { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public Post Post { get; set; } = null!;
    public int PostId { get; set; }
    public int Likes { get; set; }
}