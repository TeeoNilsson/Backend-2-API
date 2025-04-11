using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/books")]
[Authorize]

public class BookController : ControllerBase
{
    private readonly IBookService bookService;
    private readonly ILogger<BookController> logger;
    public BookController(IBookService bookService, ILogger<BookController> logger){ 
        this.bookService = bookService;
        this.logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody]CreateBookRequest request) {
        try{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null) {
                return Unauthorized();
            }
            var book = await bookService.CreateBook(request, userId);
            return CreatedAtAction(nameof(CreateBook), new {id = book.Id}, book);
        } 
        catch(ArgumentException exception) {
            return BadRequest(exception.Message);
        }
        catch(Exception exception) {
            logger.LogError("Error", exception.Message);
            return StatusCode(500, "Error");
        }
    }
}
public class CreateBookRequest {
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
}