public class BookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int Likes { get; set; }
    public List<ReviewDto> Reviews { get; set; } 
}
