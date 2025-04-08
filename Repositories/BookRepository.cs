public class BookRepository : IBookRepository
{
    public async Task<Book> AddAsync(Book book)
    {
        // TODO : DB-logik för att lägga till bok
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        // TODO: DB-logik för att hämta en bok med id från db
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        // TODO: DB-logik för att hämta alla böcker från db
    }

    public async Task<bool> LikeAsync(Guid bookId)
    {
        // TODO: Gilla en bok
    }
}