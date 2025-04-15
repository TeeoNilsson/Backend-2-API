using Microsoft.EntityFrameworkCore;

public interface IBookRepository {
    public Task Add(Book book);
    Task<int> Delete(Guid bookId, string userId);
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(Guid id);
}
