using Microsoft.AspNetCore.Mvc;

//Lägga till en review
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
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsForBook(Guid bookId)
        {
            var result = await _revireService.GetReviewsForBookIdAsunc(bookId);
            return Ok(result);
        }
    }
}
