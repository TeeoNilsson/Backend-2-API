public interface IReviewService
{
    Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId); //Hämta alla reviews på en specifik bok
    Task<Review> CreateReviewAsync(CreateReviewDto dto); // Lägg till en recension
    Task<Review> UpdateReviewAsync(Guid id, UpdateReviewDto dto); // Redigera recension
    Task DeleteReviewAsync(Guid reviewId); // Ta bort recension
    Task<Review> LikeReviewAsync(Guid reviewId); // Like/dislike logik
}
