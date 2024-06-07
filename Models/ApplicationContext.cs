using Microsoft.EntityFrameworkCore;

namespace koob_be.Models;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostComment> PostComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(
            new User
            {
                Id = 1,
                Name = "Lucas",
                UserName = "XCheesee",
                Email = "teste@teste.com",
                Pw = "Champ"
            },
            new User
            {
                Id = 2,
                Name = "Fernanda",
                UserName = "Malaquias",
                Email = "etset@etset.com",
                Pw = "Pog"
            }
        );

        modelBuilder.Entity<Post>()
        .HasData(
            new Post
            {
                Id = 1,
                Content = "Bala",
                UserId = 1,
                User = null!
            },
            new Post
            {
                Id = 2,
                Content = "Bagarai",
                UserId = 2,
                User = null!
            }
        );

        modelBuilder.Entity<PostComment>()
        .HasData(
            new PostComment
            {
                Id = 1,
                UserId = 1,
                PostId = 1,
                Comment = "pog",
                User = null!,
                Post = null!
            },
            new PostComment
            {
                Id = 2,
                UserId = 1,
                PostId = 2,
                Comment = "champ",
                User = null!,
                Post = null!

            },
            new PostComment
            {
                Id = 3,
                UserId = 2,
                PostId = 1,
                Comment = "cringe",
                User = null!,
                Post = null!
            }
        );
    }
}
