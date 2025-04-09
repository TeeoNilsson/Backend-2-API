public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByBookIdAsync(Guid bookId)
    {
        var reviews = await _reviewRepository.GetBookByIdAsync(bookId);

        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            UserId = r.UserId,
            BookId = r.BookId,
            Likes = r.Likes,
            CreatedAt = r.DateTime,
        });
    }

    public async Task<Guid> CreateReviewAsync(CreateReviewDto dto)
    {
        var newReview = new Review
        {
            Rating = dto.Rating,
            Comment = dto.Comment,
            BookId = dto.BookId,
            Likes = 0,
            DateTime = DateTime.UtcNow,
        };

        var CreatedReview = await _reviewRepository.AddReviewAsync(newReview);
        return CreatedReview.Id;
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
