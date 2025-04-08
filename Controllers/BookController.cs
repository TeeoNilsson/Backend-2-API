using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; 
using BookReviewApi.Models;
using BookReviewApi.Services;

namespace BookReviewApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        // //Get all books
        // [HttpGet]
        // public async Task<IActionResult> GetBooks()
        // {
        //     var books = await _bookService.GetAllBooksAsync();
        //     return Ok(books);
        // }

        // //Get book with id
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetBook(Guid id)
        // {
        //     var book = await _bookService.GetBookByIdAsync(id);

        //     if (book == null)
        //     {
        //         return NotFound();
        //     }


        //     return Ok(book);
        // }

        //Add a new book
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request, string UserId )
        {
            try
            {
                //change to check stored users when identity core is implemented
               if (UserId == null)
               {
                 return Unauthorized();
               }

               var book = await _bookService.CreateBook(request, UserId);
               return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        }
    }


    public class CreateBookRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
    }

    public class BookResponse
    {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Review> Reviews { get; set; }
    public string Author { get; set; }
    public int Likes { get; set; }
    }
