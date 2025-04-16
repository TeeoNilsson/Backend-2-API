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
        //Hämtar recensionerna som matchar bokens id från databasen
        //Skickar tillbaka en lista med recensioner
        return await context.Reviews
            .Where(review => review.BookId.Equals(bookId))
            .ToListAsync();
    }

    public async Task AddReviewAsync(Review review)
    {
        //Sparar en ny recension i databasen
        await context.Reviews.AddAsync(review);

        //Sparar ändringar i databasen
        await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid reviewId)
    {
        //Letar upp och tar tar bort recensionen som matchar id
        //"ExecuteDeleteAsyn" räknar också hur många objekt som raderas och vi returnerar den siffran
        return await context.Reviews.Where(review => review.Id.Equals(reviewId)).ExecuteDeleteAsync();
    }

    public async Task<Review?> FindByIdAsync(Guid id)
    {
        //Hämtar ut review från databasen som matchar med id
        return await context.Reviews.FindAsync(id);
    }

    public async Task Save()
    {
        //Sparar ändringar i databasen
        await context.SaveChangesAsync();
    }
}