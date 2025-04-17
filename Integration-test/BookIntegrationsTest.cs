using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Xunit;
public class BookIntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookIntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
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
}
