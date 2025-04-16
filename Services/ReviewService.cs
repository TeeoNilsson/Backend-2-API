using Microsoft.EntityFrameworkCore;
public class ReviewService : IReviewService
{
    private readonly IReviewRepository reviewRepository;
    private readonly IBookRepository bookRepository;

    public ReviewService(IReviewRepository reviewRepository, IBookRepository bookRepository)
    {
        this.reviewRepository = reviewRepository;
        this.bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(Guid bookId)
    {
        //Kollar om det finns en bok med bookId
        var book =
            await bookRepository.GetBookByIdAsync(bookId)
            ?? throw new KeyNotFoundException($"No book found with ID {bookId}");

        //Hämtar alla reviews för boken
        var reviews = await reviewRepository.GetBookByIdAsync(book.Id);

        //Kollar om boken har några reviews
        if (reviews == null || !reviews.Any())
        {
            throw new Exception($"No reviews found for book with ID {bookId}");
        }

        //Skickar listan med reviews till controllern
        return reviews;
    }

    public async Task<Review> CreateReviewAsync(CreateReviewDto dto)
    {
        //Validera inputs
        //Kontrollerar att rating är mellan 0 och 10
        if (dto.Rating < 0 || dto.Rating > 10)
        {
            throw new ArgumentException("Rating must be a number between 0 and 10");
        }

        //Kontrollerar att kommentaren är minst 5 tecken
        if (string.IsNullOrEmpty(dto.Comment) || dto.Comment.Length < 5)
        {
            throw new ArgumentException("Comment must not be empty or less than 5 characters");
        }

        //Kollar om det finns en bok med bookId
        var book = await bookRepository.GetBookByIdAsync(dto.BookId);
        if (book == null)
        {
            throw new KeyNotFoundException($"No book found with ID {dto.BookId}");
        }

        //Skapar en ny review från dton vi fått från controllern
        var newReview = new Review(dto.Rating, dto.Comment, dto.UserId, dto.BookId);

        try
        {
            //Skickar den nya reviewn till repository, som sparar i databasen
            await reviewRepository.AddReviewAsync(newReview);
        }
        catch (Exception ex)
        {
            // Handle database-related exceptions
            throw new InvalidOperationException($"Failed to save the review in the database", ex);
        }

        //Skickar tillbaka den nya reviewn till controllern
        return newReview;
    }

    public async Task<Review> UpdateReviewAsync(Guid id, UpdateReviewDto dto)
    {
        //Validera inputs
        //Kontrollerar att rating är mellan 0 och 10
        if (dto.Rating < 0 || dto.Rating > 10)
        {
            throw new ArgumentException("Rating must be a number between 0 and 10");
        }

        //Kontrollerar att kommentaren är minst 5 tecken
        if (string.IsNullOrEmpty(dto.Comment) || dto.Comment.Length < 5)
        {
            throw new ArgumentException("Comment must not be empty or less than 5 characters");
        }

        //Kontrollerar att kommentaren är max 100 tecken
        if (dto.Comment.Length > 100)
        {
            throw new ArgumentException("Comment can not be more than 100 characters");
        }

        //Hämtar aktuell review från databasen
        Review? review = await reviewRepository.FindByIdAsync(id);

        if (review == null)
        {
            throw new KeyNotFoundException($"No review found with ID {id}");
        }

        try
        {
            //Ändrar aktuella värden som skickats in från controllern
            review.Comment = dto.Comment;
            review.Rating = dto.Rating;

            //Sparar ändringar i databasen
            await reviewRepository.Save();
        }
        catch (Exception ex)
        {
            // Handle database-related exceptions
            throw new Exception($"Failed to update review with ID {id}", ex);
        }

        //Skickar tillbaka uppdaterad review till controllern
        return review;
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        //Hämtar aktuell review från databasen
        Review? review = await reviewRepository.FindByIdAsync(reviewId);

        if (review == null)
        {
            throw new KeyNotFoundException($"No review found with ID {reviewId}");
        }

        //Tar bort aktuell review från databasen
        int removedCount = await reviewRepository.DeleteAsync(reviewId);
        if (removedCount <= 0)
        {
            throw new Exception($"Failed to delete review with ID {reviewId}");
        }
    }

    public async Task<Review> LikeReviewAsync(Guid reviewId)
    {
        //Hämtar aktuell review från databasen
        Review? review = await reviewRepository.FindByIdAsync(reviewId);

        if (review == null)
        {
            throw new KeyNotFoundException($"No review found with ID {reviewId}");
        }

        try
        {
            //Ökar gilla antalet med 1
            review.Likes++;

            //Sparar ändringar i databasen
            await reviewRepository.Save();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update like count for review {reviewId}", ex);
        }

        //Skickar tillbaka uppdaterad review till controllern
        return review;
    }
}