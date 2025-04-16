using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// namespace bokRecension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("create_book", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddOpenApi();

// Registrering av Review Service och Repository
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Registrering av Book Service och Repository
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, EFBookRepository>();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddIdentityCore<User>()
.AddEntityFrameworkStores<AppDbContext>()
.AddApiEndpoints();
builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
