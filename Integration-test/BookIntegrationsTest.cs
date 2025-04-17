using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Net;
using System.Drawing;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Runtime.CompilerServices;
public class BookIntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookIntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact] //Testar att skapa en bok med rätt input ska ge rätt.
    public async Task CreateBook_ReturnsSuccess()
    {
        var request = new CreateBookDto
        {
            Title = "Integration Test Book",
            Description = "Testing description",
            Author = "Jane Doe",
            UserId = Guid.NewGuid() // lägg till en användare om du behöver validera det
        };

        var response = await _client.PostAsJsonAsync("/api/books", request);

        response.EnsureSuccessStatusCode();
        var book = await response.Content.ReadFromJsonAsync<BookDto>();
        Assert.Equal(request.Title, book.Title);
    }

    [Fact] //Testar att skapa en bok med felaktig input ska ge fel.
        public async Task CreateBook_ReturnFail()
    {
        var request = new CreateBookDto
        {
            Title = "Integration Test Book",
            Author = "Jane Doe",
            UserId = Guid.NewGuid() // lägg till en användare om du behöver validera det
        };

        var response = await _client.PostAsJsonAsync("/api/books", request);

        Assert.False(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteBook_ShouldReturnOk()
    {
        var userId = Guid.NewGuid().ToString();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", userId);

        var request = new CreateBookRequest
        {
            Title = "Test title",
            Description = "Test beskrivning",
            Author = "Test"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/books", request);
        createResponse.EnsureSuccessStatusCode();

        var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();

        var deleteResponse = await _client.DeleteAsync($"/api/books/{createdBook.Id}");

        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task CreateReview_ReturnsSuccess()
    {
        var userId = Guid.NewGuid();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", userId.ToString());
        
        var createBookRequest = new CreateBookRequest
        {
            Title = "Test title",
            Description = "Test beskrivning",
            Author = "Test"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/books", createBookRequest);
        createResponse.EnsureSuccessStatusCode();

        var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();


        var createReviewRequest = new CreateReviewDto
        {
            Rating = 1,
            Comment = "Testar",
            UserId = userId,
            BookId = createdBook.Id
        };

        var createReviewResponse = await _client.PostAsJsonAsync("/api/reviews", createReviewRequest);

        
        createReviewResponse.EnsureSuccessStatusCode();
        var getResponse = await createReviewResponse.Content.ReadFromJsonAsync<ReviewDto>();

        
        Assert.NotNull(getResponse);
        Assert.Equal("Testar", getResponse.Comment);
        Assert.Equal(1, getResponse.Rating);
        Assert.Equal(createdBook.Id, getResponse.BookId);
        Assert.Equal(userId, getResponse.UserId);
    }

}
