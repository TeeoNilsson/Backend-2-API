
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BookReviewApi.Models
{

public class BookEntity
{
    //userId
    public string UserId { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Review> Reviews { get; set; }
    public string Author { get; set; }

    public int Likes { get; set; }

    public BookEntity(string title, string description, string author, string userId)
    {
        this.Title = title;
        this.UserId = userId;
        this.Description = description;
        this.Likes = 0;
        this.Author = author;
        this.Reviews = new List<Review>();
    }

    public BookEntity()
    { 
        this.Title = string.Empty;
        this.UserId = string.Empty;
        this.Description = string.Empty;
        this.Author = string.Empty;
        this.Reviews = new List<Review>();
        this.Likes = 0;
    }
}

}