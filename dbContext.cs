using BookReviewApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<BookEntity> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
}
