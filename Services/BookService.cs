public class BookService : IBookService
{
    public async Task<Book> AddBookAsync(Book book)
    {
        // TODO: Lägga till en bok
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        // TODO: Hämta bok med specifikt id
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        // TODO: Hämta alla böcker
    }

    public async Task<bool> LikeBookAsync(Guid bookId)
    {
        // Gilla en bok
    }



}