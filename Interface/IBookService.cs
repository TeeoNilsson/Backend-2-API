
public interface IBookService
{
    Task<Book> CreateBook(CreateBookRequest request, string userId);
    Task DeleteBook(Guid bookId, string userId);
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookByIdAsync(Guid id);
    Task<bool> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto);
}

