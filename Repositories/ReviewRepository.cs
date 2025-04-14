using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext context;

    public ReviewRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Review>> GetBookByIdAsync(Guid bookId)
    {
        //Tar in bok id
        //Hämtar recensionerna från databasen
        //Skickar tillbaka en lista med recensioner
        return await context.Reviews
            .Where(review => review.BookId.Equals(bookId))
            .ToListAsync();
    }

    public async Task AddReviewAsync(Review review)
    {
        //Sparar en ny recension i databasen
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateReviewDto dto)
    {
        //Hitta reviewn med samma id
        var review = await context.Reviews.Where(review => review.Id.Equals(id)).FirstOrDefaultAsync();

        //Om det inte finns en review med samma id
        if (review == null)
        {
            return false;
        }

        //Annars uppdatera kommentaren och ratingen
        review.Comment = dto.Comment;
        review.Rating = dto.Rating;

        //Och spara ändringarna
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> DeleteAsync(Guid reviewId)
    {
        //Tar in recensions id
        //Letar upp och tar tar bort recensionen
        //"ExecuteDeleteAsyn" räknar också hur många objekt som raderas och vi returnerar den siffran
        return await context.Reviews.Where(review => review.Id.Equals(reviewId)).ExecuteDeleteAsync();
    }

    public async Task<bool> LikeAsync(Guid reviewId)
    {
        //Hitta reviewn med samma id
        var review = await context.Reviews.Where(review => review.Id.Equals(reviewId)).FirstOrDefaultAsync();

        //Om det inte finns en review med samma id
        if (review == null)
        {
            return false;
        }

        //Annars öka like antalet 
        review.Likes++;

        //Och spara ändringarna
        await context.SaveChangesAsync();
        return true;
    }
}