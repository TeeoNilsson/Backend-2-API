public class Review
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public int Likes { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;

    public Review(int rating, string comment, Guid userid, Guid bookid)
    {
        this.Id = new Guid();
        this.Rating = rating;
        this.Comment = comment;
        this.UserId = userid;
        this.BookId = bookid;
        this.Likes = 0;
        this.DateTime = DateTime.UtcNow;
    }

    public Review() { }
}
