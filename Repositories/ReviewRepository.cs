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

    public async Task<bool> UpdateAsync(Review review)
    {
        // TODO: Implementera DB-logik för att redigera recensioner
        //Ta in dto med den nya infon?
        //Hitta reviewn med samma id
        //Uppdatera kommentaren (eller andra aktuella fält)
        //Spara ändringarna
        throw new NotImplementedException();
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
        // TODO: Implemenetera DB-logik för att gilla en recension
        //Tar in ett recensions id
        //Skickar tillbaka true?

        //Hitta reviewn med samma id
        //Öka like antalet 
        //Spara ändringarna
        throw new NotImplementedException();
    }
}