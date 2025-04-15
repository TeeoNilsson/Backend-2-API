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

    public async Task DeleteBook(Guid bookId, string userId)
    {
        int deleted = await bookRepository.Delete(bookId, userId);
        if (deleted == 0)
        {
            throw new ArgumentException("Book not found");
        }
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await bookRepository.GetAllBooksAsync();

        return books.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Reviews = book.Reviews.ToList(),
            Author = book.Author,
            Likes = book.Likes,
            UserId = book.UserId
        });
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
        var book = await bookRepository.GetBookByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Reviews = book.Reviews.ToList(),
            Author = book.Author,
            Likes = book.Likes,
            UserId = book.UserId
        };
    }

    public async Task<bool> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto)
    {
        var existingBook = await bookRepository.GetBookByIdAsync(id);

        if (existingBook == null)
        {
            return false;
        }

        existingBook.Title = updateBookDto.Title ?? existingBook.Title;
        existingBook.Author = updateBookDto.Author ?? existingBook.Author;
        existingBook.Description = updateBookDto.Description ?? existingBook.Description;

        return await bookRepository.UpdateBookAsync(existingBook);
    }

}