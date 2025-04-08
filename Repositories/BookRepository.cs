using Microsoft.EntityFrameworkCore;
public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Book> AddAsync(Book book)
    {
        // TODO : DB-logik för att lägga till bok
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        // TODO: DB-logik för att hämta en bok med id från db
        return await _context.Books
        .Include(b => b.Reviews) //Hämta recensioner
        .FirstOrDefaultAsync(b => b.Id == id);
        // .FindAsync(id) inkluderar inte data som reviews
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        // TODO: DB-logik för att hämta alla böcker från db
        return await _context.Books
        .Include(b => b.Reviews) //Hämta recensioner
        .ToListAsync();
    }

    public async Task<bool> LikeAsync(Guid bookId)
    {   // TODO: Gilla en bok
        var book = await _context.Books.FindAsync(bookId);

        if (book == null)
            return false;

        book.Likes++;
        await _context.SaveChangesAsync();
        return true;
    }
}