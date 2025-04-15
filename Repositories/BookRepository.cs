using Microsoft.EntityFrameworkCore;
public class EFBookRepository : IBookRepository {
    private readonly AppDbContext context;
    public EFBookRepository(AppDbContext context) {
        this.context = context;
    }
    public async Task Add(Book book) {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task<int> Delete(Guid bookId, string userId)
    {
        int deleted = await context
            .Books.Where(book => book.Id.Equals(bookId) && book.UserId.Equals(userId))
            .ExecuteDeleteAsync();

        return deleted;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await context.Books
            .Include(b => b.Reviews)
            .ToListAsync();
    }

    public async Task<Book> GetBookByIdAsync(Guid id)
    {
        return await context.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}