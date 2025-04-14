public interface IBookService
{
    Task<BookDto> AddBookAsync(CreateBookDto dto); // Lägg till bok
    Task<BookDto?> GetBookByIdAsync(Guid id); // Hämta bok med specifikt id
    Task<IEnumerable<BookDto>> GetAllBooksAsync(); // Hämta alla böcker
    Task<bool> LikeBookAsync(Guid bookId); // Gilla bok
}