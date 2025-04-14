public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Book> AddBookAsync(Book book)
    {
        return book;
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        // TODO: Hämta bok med specifikt id
        return null!;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        // TODO: Hämta alla böcker
        return new List<Book>();
    }

    public async Task<bool> LikeBookAsync(Guid bookId)
    {
        // Gilla en bok
        return false;
    }
}