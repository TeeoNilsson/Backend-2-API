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

        //Skapar en ny review från dton vi fått från controllern
        var newReview = new Review(dto.Rating, dto.Comment, dto.UserId, dto.BookId);

        //Skickar den nya reviewn till repository, som sparar i databasen
        await reviewRepository.AddReviewAsync(newReview);

        //Skickar tillbaka den nya reviewn till controllern
        return newReview;
    }

    public async Task<Review> UpdateReviewAsync(Guid id, UpdateReviewDto dto)
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

        //Hämtar aktuell review från databasen
        Review? review = await reviewRepository.FindByIdAsync(id);

        if (review == null)
        {
            throw new KeyNotFoundException("Review does not exist.");
        }

        //Ändrar aktuella värden som skickats in från controllern
        review.Comment = dto.Comment;
        review.Rating = dto.Rating;

        //Sparar ändringar i databasen
        await reviewRepository.Save();

        //Skickar tillbaka uppdaterad review till controllern
        return review;
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        //Tar bort aktuell review från databasen
        int removedCount = await reviewRepository.DeleteAsync(reviewId);
        if (removedCount <= 0)
        {
            throw new KeyNotFoundException("Review does not exist.");
        }
    }

    public async Task<Review> LikeReviewAsync(Guid reviewId)
    {
        //Hämtar aktuell review från databasen
        Review? review = await reviewRepository.FindByIdAsync(reviewId);

        if (review == null)
        {
            throw new KeyNotFoundException("Review does not exist.");
        }

        //Ökar gilla antalet med 1
        review.Likes++;

        //Sparar ändringar i databasen
        await reviewRepository.Save();

        //Skickar tillbaka uppdaterad review till controllern
        return review;
    }
}