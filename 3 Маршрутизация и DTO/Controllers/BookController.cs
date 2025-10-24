using _3_Маршрутизация_и_DTO.Valitador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Net;
using System.Runtime;
using _3_Маршрутизация_и_DTO;

namespace _3_Маршрутизация_и_DTO.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class BookController : ControllerBase
    {
        private readonly MySettings _settings;
        private static readonly LinkedList<Book> _books = new();

        public BookController(IOptions<MySettings> setting)
        {
            _settings = setting.Value;
        }
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            await Task.Delay(1000);
            return Ok(new
            {
                ApplicationName = _settings.ApplicationName,
                MaxBooksPerPage = _settings.MaxBooksPerPage,
                ApiSettings = _settings.ApiSettings
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> Get()
        {
            await Task.Delay(1000);
            var books = _books.Select(b => new BookResponseDTO(b.Id, b.Year, b.Author, b.Title));
            return Ok(books);
        }
        [HttpGet("{id:guid}")]

        public async Task<ActionResult<BookResponseDTO>> GetbyId([FromRoute] Guid id)
        {
            await Task.Delay(1000);
            var b = _books.FirstOrDefault(b => b.Id == id);
            if (b == null)
                return NotFound();


            var book = new BookResponseDTO(b.Id, b.Year, b.Author, b.Title);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookResponseDTO>> CreateBook([FromBody] CreateBookDTO newBook)
        {
            await Task.Delay(1000);
            var b = new Book(newBook.year, newBook.author, newBook.title);
            _books.AddLast(b);

            var book = new BookResponseDTO(b.Id, b.Year, b.Author, b.Title);

            return Ok(book);
        }

        [HttpPut("id:guid")]
        public async Task<IActionResult> Update([FromBody] UpdateBookDTO book)
        {
            await Task.Delay(1000);
            var b = _books.FirstOrDefault(b => b.Id == book.id);
            if (b == null)
                return NotFound();
            b.Year = book.year;
            b.Author = book.author;
            b.Title = book.title;

            return NoContent();

        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            await Task.Delay(1000);
            var b = _books.FirstOrDefault(b => b.Id == id);
            if (b == null)
                return NotFound();
            _books.Remove(b);

            return NoContent();


        }
        [HttpGet("error")]
        public async Task<IActionResult> DivideByZero()
        {
            await Task.Delay(1000);

            throw new InvalidOperationException();
        }


    }
}
