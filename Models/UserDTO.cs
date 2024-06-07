namespace koob_be.Models;

public class UserDTO
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public string? Name { get; set; }
}