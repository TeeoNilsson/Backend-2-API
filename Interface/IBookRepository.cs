public interface IBookRepository
{
    Task<Book> AddAsync(Book book); // TODO: Lägg till bok i databasen
    Task<Book?> GetByIdAsync(Guid id); // TODO: Hämta bok med id från databasen
    Task<IEnumerable<Book>> GetAllAsync(); // TODO: Hämta alla böcker från databasen
    Task<bool> LikeAsync(Guid bookId); // TODO: Likes för en bok
}
