using BookReviewApi.Models;
using Microsoft.EntityFrameworkCore;

public interface IBookRepository
{
    public Task Add(BookEntity entity);

    // Task<Book> AddAsync(Book book); // TODO: Lägg till bok i databasen
    // Task<Book?> GetByIdAsync(Guid id); // TODO: Hämta bok med id från databasen
    // Task<IEnumerable<Book>> GetAllAsync(); // TODO: Hämta alla böcker från databasen
    // Task<bool> LikeAsync(Guid bookId); // TODO: Likes för en bok
}

public class EFBookRepository : IBookRepository
{
    private readonly AppDbContext context;

    public EFBookRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task Add(BookEntity entity)
    {
        await context.Books.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}