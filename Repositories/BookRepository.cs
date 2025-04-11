using Microsoft.EntityFrameworkCore;
public class EFBookRepository : IBookRepository {
    private readonly AppDbContext context;
    public EFBookRepository(AppDbContext context) {
        this.context = context;
    }
    public async Task Add(Book book) {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }
}