public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public async ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
    {
        // TODO: Implementera logik för att hämta alla recensioner för en bok
        throw new NotImplementedException();
    }

    public async Task<ReviewService> AddReviewAsync(Review review)
    {
        //TODO: Implementera logik för att lägga till en recension
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateReviewAsync(Review review)
    {
        // TODO: Implementera logik för att redigera recension
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteReviewAsync(Guid reviewId)
    {
        // TODO: Implementera logik för att radera recension
        throw new NotImplementedException();
    }

    public async Task<bool> LikeReviewAsync(Guid reviewId)
    {
        // TODO: Implementera logik för att gilla recensioner
        throw new NotImplementedException();
    }


}