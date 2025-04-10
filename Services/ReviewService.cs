using Microsoft.EntityFrameworkCore;
public class ReviewService : IReviewService
{
    private readonly IReviewRepository reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        this.reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
    {
        //TODO
        //Kolla om det finns en book med dto.BookId här

        //Hämtar alla reviews för boken
        var reviews = await reviewRepository.GetBookByIdAsync(bookId);

        //Kollar om boken har några reviews
        if (reviews == null || !reviews.Any())
        {
            throw new KeyNotFoundException($"No reviews found for book with ID {bookId}");
        }

        return reviews;
    }

    public async Task<Review> CreateReviewAsync(CreateReviewDto dto)
    {
        //Validera information
        //Kontrollerar att rating är mer än 0
        if (dto.Rating <= 0)
        {
            throw new ArgumentException("Rating must be more than 0");
        }

        //Kontrollerar att kommentaren är minst 5 tecken
        if (string.IsNullOrEmpty(dto.Comment) || dto.Comment.Length < 5)
        {
            throw new ArgumentException("Comment must not be empty or less than 5 characters");
        }

        //TODO
        //Kolla om det finns en book med dto.BookId här
        //Kolla om det finns en användare med dto.UserId här - (alt auth?)

        //Skapar en ny review från dton vi fått från controllern
        var newReview = new Review(dto.Rating, dto.Comment, dto.UserId, dto.BookId);

        //Skickar den nya reviewn till repository, som sparar i databasen
        await reviewRepository.AddReviewAsync(newReview);

        //Skickar tillbaka den nya reviewn till controllern
        return newReview;
    }

    public async Task<bool> UpdateReviewAsync(Review review)
    {
        // TODO: Implementera logik för att redigera recension
        //Behöver jag ta in den gamla och en ny review, eller den gamla samt aktuell info/dto?
        throw new NotImplementedException();
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        // TODO: Implementera logik för att radera recension
        int removedCount = await reviewRepository.DeleteAsync(reviewId);
        if (removedCount <= 0)
        {
            throw new KeyNotFoundException("Review does not exist.");
        }
    }

    public async Task<bool> LikeReviewAsync(Guid reviewId)
    {
        // TODO: Implementera logik för att gilla recensioner
        throw new NotImplementedException();
    }
}
