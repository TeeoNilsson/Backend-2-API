public class CreateReviewDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}
