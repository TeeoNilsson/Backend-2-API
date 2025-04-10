using Microsoft.AspNetCore.Mvc;

//Like/Dislike
//Ta bort review
//Redigera review

namespace Backend_2_API.Controllers
{
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
        // [Authorize] Lägga till när Identity är klart!
        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            try
            {
            var createdId = await _reviewService.CreateReviewAsync(dto);
            return createdIdAtAction(nameof(GetReviewsForBook), new {bookId = dto.BookId}, new {id = createdId})
            }
            catch(ArgumentExeption ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Ett oväntat fel inträffade")
            }

        }

        //Redigera review

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
    }
}
