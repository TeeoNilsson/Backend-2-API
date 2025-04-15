public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetBookByIdAsync(Guid bookId); // Hämta recensioner till en bok
    Task AddReviewAsync(Review review); // Lägg till recension
    Task<int> DeleteAsync(Guid reviewId); // Ta bort recension
    Task<Review?> FindByIdAsync(Guid id); // Hitta review med id
    Task Save(); // Sparar ändringar i databasen
}