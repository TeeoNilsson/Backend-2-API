public class ReviewDto
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
}
