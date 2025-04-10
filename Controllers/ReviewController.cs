using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.AddAuthorization;

//Like/Dislike
//Ta bort review
//Redigera review

namespace Backend_2_API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    [Authorize]
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
        [HttpPost]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            var createdId = await _reviewService.CreateReviewAsync(dto);
            return createdIdAtAction(nameof(GetReviewsForBook), new {bookId = dto.BookId}, new {id = createdId})
        }
    }
}
