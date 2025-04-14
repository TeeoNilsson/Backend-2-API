public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookDto> AddBookAsync(CreateBookDto dto)
    {
        // Här antar vi att User hämtas av UserId – beroende på hur du hanterar det
        var user = new User { Id = dto.UserId }; // Du kan behöva hämta detta från DB

        var book = new Book(dto.Title, dto.Description, dto.Author, user);
        var createdBook = await _bookRepository.AddAsync(book);

        return new BookDto
        {
            Id = createdBook.Id,
            Title = createdBook.Title,
            Description = createdBook.Description,
            Author = createdBook.Author,
            Likes = createdBook.Likes,
            Reviews = new List<ReviewDto>() // tom från början
        };
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null) return null;

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Author = book.Author,
            Likes = book.Likes,
            Reviews = book.Reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                Likes = r.Likes,
                CreatedAt = r.DateTime
            }).ToList()
        };
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Author = book.Author,
            Likes = book.Likes,
            Reviews = book.Reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                Likes = r.Likes,
                CreatedAt = r.DateTime
            }).ToList()
        });
    }

    public async Task<bool> LikeBookAsync(Guid bookId)
    {
        return await _bookRepository.LikeAsync(bookId);
    }
}
