using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;

namespace _3_Маршрутизация_и_DTO.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class BookController : ControllerBase
    {
        private static readonly LinkedList<Book> _books = new();


        [HttpGet]
        public ActionResult<IEnumerable<ResponseBookDTO>> Get()
        {
            var books = _books.Select(b => new ResponseBookDTO(b.Id, b.Year, b.Author, b.Title));
            return Ok(books);
        }
        [HttpGet("{id:guid}")]
        public ActionResult<ResponseBookDTO> GetbyId([FromRoute] Guid id)
        {

            var b = _books.FirstOrDefault(b => b.Id == id);
            if (b == null)
                return NotFound();


            var book = new ResponseBookDTO(b.Id, b.Year, b.Author, b.Title);
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<ResponseBookDTO> CreateBook([FromBody] CreateBookDTO newBook)
        {
            var b = new Book(newBook.year, newBook.author, newBook.title);
            _books.AddLast(b);

            var book = new ResponseBookDTO(b.Id, b.Year, b.Author, b.Title);

            return Ok(book);
        }

        [HttpPut("id:guid")]
        public IActionResult Update([FromBody] UpdateBookDTO book)
        {
            var b = _books.FirstOrDefault(b => b.Id == book.id);
            if (b == null)
                return NotFound();
            b.Year = book.year;
            b.Author = book.author;
            b.Title = book.title;

            return NoContent();

        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var b = _books.FirstOrDefault(b => b.Id == id);
            if (b == null)
                return NotFound();
            _books.Remove(b);

            return NoContent();


        }
        [HttpGet("error")]
        public IActionResult DivideByZero()
        {

            throw new InvalidOperationException();
        }


    }
}
