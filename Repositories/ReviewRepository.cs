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

    /* public async Task<Review> UpdateAsync(Guid id, UpdateReviewDto dto)
    {
        //Hitta reviewn med samma id
        var review = await context.Reviews.Where(review => review.Id.Equals(id)).FirstOrDefaultAsync();

        //Om det inte finns en review med samma id

        //Annars uppdatera kommentaren och ratingen
        review.Comment = dto.Comment;
        review.Rating = dto.Rating;

        //Och spara ändringarna
        await context.SaveChangesAsync();
        return review;
    } */

    public async Task<int> DeleteAsync(Guid reviewId)
    {
        //Tar in recensions id
        //Letar upp och tar tar bort recensionen
        //"ExecuteDeleteAsyn" räknar också hur många objekt som raderas och vi returnerar den siffran
        return await context.Reviews.Where(review => review.Id.Equals(reviewId)).ExecuteDeleteAsync();
    }

    /* public async Task<Review> LikeAsync(Guid reviewId)
    {
        //Hitta reviewn med samma id
        var review = await context.Reviews.Where(review => review.Id.Equals(reviewId)).FirstOrDefaultAsync();

        //Annars öka like antalet 
        review.Likes++;

        //Och spara ändringarna
        await context.SaveChangesAsync();
        return review;
    } */

    public async Task<Review?> FindByIdAsync(Guid id)
    {
        return await context.Reviews.FindAsync(id);
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }
}