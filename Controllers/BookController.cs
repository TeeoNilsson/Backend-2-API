using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.AddAuthorization;

[Route("api/books")]
[ApiController]
public class BookController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await db.Books.ToListAsync();
    }
}


// Hämta en bok med specifikt id - inkludera recensioner
// Lägga till en bok
// Uppdatera en bok
// Radera en bok
// Like/Dislike
