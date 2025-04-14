using Microsoft.EntityFrameworkCore;
public class ReviewService : IReviewService
{
    private readonly IReviewRepository reviewRepository;
    private readonly IBookService bookService;

    public ReviewService(IReviewRepository reviewRepository, IBookService bookService)
    {
        this.reviewRepository = reviewRepository;
        this.bookService = bookService;
    }

    public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
    {
        //Kollar om det finns en bok med bookId
        var book =
            await bookService.GetBookByIdAsync(bookId)
            ?? throw new ArgumentNullException("Book not found");

        //Hämtar alla reviews för boken
        var reviews = await reviewRepository.GetBookByIdAsync(book.Id);

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

        if (dto.Rating > 10)
        {
            throw new ArgumentException("Rating can not be more than 10");
        }

        //Kontrollerar att kommentaren är minst 5 tecken
        if (string.IsNullOrEmpty(dto.Comment) || dto.Comment.Length < 5)
        {
            throw new ArgumentException("Comment must not be empty or less than 5 characters");
        }

        //Kollar om det finns en bok med bookId
        var book =
            await bookService.GetBookByIdAsync(dto.BookId)
            ?? throw new ArgumentNullException("Book not found");

        //Kolla om det finns en användare med dto.UserId här?

        //Skapar en ny review från dton vi fått från controllern
        var newReview = new Review(dto.Rating, dto.Comment, dto.UserId, dto.BookId);

        //Skickar den nya reviewn till repository, som sparar i databasen
        await reviewRepository.AddReviewAsync(newReview);

        //Skickar tillbaka den nya reviewn till controllern
        return newReview;
    }

    public async Task<bool> UpdateReviewAsync(Guid id, UpdateReviewDto dto)
    {
        //Validera information
        //Kontrollerar att rating är mer än 0
        if (dto.Rating <= 0)
        {
            /* throw new ArgumentException("Rating must be more than 0"); */
            return false;
        }

        if (dto.Rating > 10)
        {
            /* throw new ArgumentException("Rating can not be more than 10"); */
            return false;
        }

        //Kontrollerar att kommentaren är minst 5 tecken
        if (string.IsNullOrEmpty(dto.Comment) || dto.Comment.Length < 5)
        {
            /* throw new ArgumentException("Comment must not be empty or less than 5 characters"); */
            return false;
        }

        return await reviewRepository.UpdateAsync(id, dto);
    }

    public async Task<bool> DeleteReviewAsync(Guid reviewId)
    {
        // TODO: Implementera logik för att radera recension
        int removedCount = await reviewRepository.DeleteAsync(reviewId);
        if (removedCount <= 0)
        {
            // throw new KeyNotFoundException("Review does not exist.");
            return false;
        }
        return true;
    }

    public async Task<bool> LikeReviewAsync(Guid reviewId)
    {
        // TODO: Implementera logik för att gilla recensioner
        return await reviewRepository.LikeAsync(reviewId);
    }
}
