//Lägga till en review
//Like/Dislike
//Ta bort review
//Redigera review
//Hämta alla reviews på en specifik bok

[Route("api/reviews")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }
}
