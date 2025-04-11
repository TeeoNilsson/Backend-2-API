using Microsoft.EntityFrameworkCore;

public interface IBookRepository {
    public Task Add(Book book);
}
