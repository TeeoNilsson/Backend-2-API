using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetBookByIdAsync(Guid bookId)
    {
        return await _context.Reviews.Where(r => r.BookId == bookId).ToListAsync();
    }

    public async Task<Review> AddAsync(ReviewRepository review)
    {
        // TODO: Implementera DB-logik för att lägga till recension
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Review review)
    {
        // TODO: Implementera DB-logik för att redigera recensioner
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid reviewId)
    {
        // TODO: Implementera DB-logik för att radera recensioner
        throw new NotImplementedException();
    }

    public async Task<bool> LikeAsync(Guid reviewId)
    {
        // TODO: Implemenetera DB-logik för att gilla en recension
        throw new NotImplementedException();
    }
}
