using Microsoft.AspNetCore.Mvc;

[Route("api/reviews")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    //Hämta alla reviews på en specifik bok
    [HttpGet("books/{bookId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForBook(Guid bookId)
    {
        var result = await _reviewService.GetReviewsByBookIdAsync(bookId);

        return Ok(result);
    }

    //Lägga till en review
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateReview([FromBody] CreateReviewDto dto)
    {
        try
        {
            var createdId = await _reviewService.CreateReviewAsync(dto);
            return CreatedAtAction(
                nameof(GetReviewsForBook),
                new { bookId = dto.BookId },
                new { id = createdId }
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ett oväntat fel inträffade");
        }
    }

    //Redigera review
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(Guid id, [FromBody] UpdateReviewDto dto)
    {
        var success = await _reviewService.UpdateReviewAsync(id, dto);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [Authorize]
    //Ta bort review
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        var success = await _reviewService.DeleteReviewAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [Authorize]
    //Like/Dislike
    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikeReview(Guid id)
    {
        var success = await _reviewService.LikeReviewAsync(id);

        if (!success)
        {
            return NotFound();
        }
        return Ok();
    }
}
