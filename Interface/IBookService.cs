public interface IBookService
{
    public Task<Book> CreateBook(CreateBookRequest request, string userId);
}

