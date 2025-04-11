public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;
    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }
    public async Task<Book> CreateBook(CreateBookRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Title))
        {
            throw new ArgumentException("Title may not be null or empty");
        }
        if (string.IsNullOrEmpty(request.Author))
        {
            throw new ArgumentException("Author may not be null or empty");
        }
        if (string.IsNullOrEmpty(request.Description))
        {
            throw new ArgumentException("Description may not be null or empty");
        }
        var book = new Book(request.Title, request.Description, request.Author, userId);
        await bookRepository.Add(book);
        return book;
    }
}