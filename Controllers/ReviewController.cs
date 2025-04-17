using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/reviews")]
[ApiController]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IReviewService reviewService, ILogger<ReviewController> logger)
    {
        _reviewService = reviewService;
        _logger = logger;
    }

    //Hämta alla reviews på en specifik bok
    [HttpGet("books/{bookId}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForBook(Guid bookId)
    {
        try
        {
            var result = await _reviewService.GetReviewsByBookIdAsync(bookId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Book not found: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting reviews for book {BookId}", bookId);
            return StatusCode(500, "Unexpected error");
        }
    }

    //Lägga till en review
    [HttpPost]
    public async Task<ActionResult> CreateReview([FromBody] CreateReviewDto dto)
    {
        try
        {
            var createdId = await _reviewService.CreateReviewAsync(dto);
            return CreatedAtAction(
                nameof(GetReviewsForBook),
                new { bookId = dto.BookId },
                createdId
            );
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Validation error when creating review: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Book not found: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when creating review");
            return StatusCode(500, "Unexpected error");
        }
    }

    //Redigera review
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(Guid id, [FromBody] UpdateReviewDto dto)
    {
        try
        {
            var updatedReview = await _reviewService.UpdateReviewAsync(id, dto);
            return Ok(updatedReview);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Validation error when updating review: {Message}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Review not found: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating review with ID {id}", id);
            return StatusCode(500, "Unexpected error");
        }
    }

    //Ta bort review
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        try
        {
            await _reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Review not found for deletion: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting review {id}", id);
            return StatusCode(500, "Unexpected error");
        }
    }

    //Like/Dislike
    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikeReview(Guid id)
    {
        try
        {
            var likedReview = await _reviewService.LikeReviewAsync(id);
            return Ok(likedReview);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("Review not found for like: {Message}", ex.Message);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error liking review {id}", id);
            return StatusCode(500, "Unexpected error");
        }
    }
}
