public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Review> Reviews { get; set; }
    public string Author { get; set; }
    public Book(string title, string description, string author)
    {
        this.Id = new Guid();
        this.Title = title;
        this.Description = description;
        this.Author = author;
        this.Reviews = new List<Review>();
    };

    public Book() { };
}
