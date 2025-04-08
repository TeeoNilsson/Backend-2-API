public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Review> Reviews { get; set; }
    public string Author { get; set; }

    public int Likes { get; set; }

    public Book(string title, string description, string author)
    {
        this.Title = title;
        this.Description = description;
        this.Likes = 0;
        this.Reviews = new List<Review>();
    }

    public Book() { }
}
