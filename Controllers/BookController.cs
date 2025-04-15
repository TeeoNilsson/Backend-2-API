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

    [HttpDelete("{bookId}")]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            await bookService.DeleteBook(bookId, userId);
            return Ok();
        }
        catch (ArgumentNullException exception)
        {
            return NotFound(exception.Message);
        }
        catch (Exception exception)
        {
            logger.LogError("Unexpected error when deleting: {}", exception.Message);
            return StatusCode(500, "Unexpected error");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var books = await bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{bookId}")]
    public async Task<ActionResult<BookDto>> GetBookById(Guid bookId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var book = await bookService.GetBookByIdAsync(bookId);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPut("{bookId}")]
    public async Task<IActionResult> UpdateBook(Guid bookId, UpdateBookDto updateBookDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var existingBook = await bookService.GetBookByIdAsync(bookId);
        if (existingBook == null)
        {
            return NotFound();
        }

        if (existingBook.UserId != userId)
        {
            return Unauthorized();
        }

        var result = await bookService.UpdateBookAsync(bookId, updateBookDto);

        if (result)
        {
            return NoContent();
        }
        else
        {
            return BadRequest("Failed to update the book");
        }
    }



}


public class CreateBookRequest {
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
}
