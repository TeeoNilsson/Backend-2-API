using BookReviewApi.Models;

public interface IBookService
{
    public Task<BookEntity> CreateBook(CreateBookRequest request, string userId);
}

namespace BookReviewApi.Services
{
public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookEntity> CreateBook(CreateBookRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Title))
        {
            throw new ArgumentException("Title may not be null or empty");
        }
        if ( string.IsNullOrEmpty(request.Description))
        {
            throw new ArgumentException("Description may not be null or empty");
        }
        if ( string.IsNullOrEmpty(request.Author))
        {
            throw new ArgumentException("Author may not be null or empty");
        }

        //Add logic for adding userId from identity core.

        var book = new BookEntity(request.Title, request.Description, request.Author, userId);
        await _bookRepository.Add(book);
        return book;
    }


    // public async Task<Book?> GetBookByIdAsync(Guid id)
    // {
    //     // TODO: Hämta bok med specifikt id
    // }

    // public async Task<IEnumerable<Book>> GetAllBooksAsync()
    // {
    //     // TODO: Hämta alla böcker
    // }

    // public async Task<bool> LikeBookAsync(Guid bookId)
    // {
    //     // Gilla en bok
    // }


}
}