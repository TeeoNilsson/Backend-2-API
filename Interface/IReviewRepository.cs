public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetBookByIdAsync(Guid bookId); // Hämta recnsioner till en bok
    Task AddReviewAsync(Review review); // Lägg till recension
    Task<bool> UpdateAsync(Review review); // Redigera recension
    Task<int> DeleteAsync(Guid reviewId); // Ta bort recension
    Task<bool> LikeAsync(Guid reviewId); // Gilla recension
}