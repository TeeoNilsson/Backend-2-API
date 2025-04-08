public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        // TODO: Lägga till en bok
        return await _bookRepository.AddAsync(book);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        // TODO: Hämta bok med specifikt id
        return _bookRepository.Get
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        // TODO: Hämta alla böcker
        return _bookRepository.GetAllBooksAsync();
    }

    public async Task<bool> LikeBookAsync(Guid bookId)
    {
        // Gilla en bok
    }



}