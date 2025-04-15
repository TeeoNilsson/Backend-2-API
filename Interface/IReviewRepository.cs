public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetBookByIdAsync(Guid bookId); // Hämta recnsioner till en bok
    Task AddReviewAsync(Review review); // Lägg till recension
    Task<Review> UpdateAsync(Guid id, UpdateReviewDto dto); // Redigera recension
    Task<int> DeleteAsync(Guid reviewId); // Ta bort recension
    Task<Review> LikeAsync(Guid reviewId); // Gilla recension
}