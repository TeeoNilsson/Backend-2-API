public class ReviewRepository : IReviewRepository
{
    public async Task<IEnumerable<Review>> GetBookByIdAsync(Guid bookId)
    {
         // TODO: Implementera DB-logik för att hämta recensioner för att hämta recensioner till en bok
        throw new NotImplementedException();
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