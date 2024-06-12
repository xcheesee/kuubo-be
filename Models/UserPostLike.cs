using System.ComponentModel.DataAnnotations.Schema;

namespace koob_be.Models;


public class UserPostLike
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
};
