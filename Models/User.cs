using System.ComponentModel.DataAnnotations.Schema;

namespace koob_be.Models;
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string UserName { get; set; }
    public string? Name { get; set; }
    public required string Email { get; set; }
    public required string Pw { get; set; }
}