public class BookDto()
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Review> Reviews { get; set; }
    public string Author { get; set; }
    public int Likes { get; set; }
    public string UserId { get; set; }

}