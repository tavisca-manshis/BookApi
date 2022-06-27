using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;
        string[] allowedGenre = { "SciFi", "Horror", "Romance"};

        public BookController(BookContext bookContext) {
            _context = bookContext;
        }

        [HttpGet]
        public List<Book> getBooks()
        {
           var books = _context.Book.ToList();
           return books;
        }

        [Route("getBook/{id}")]
        [HttpGet]
        public async Task<IActionResult> getBookById(long id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Route("getBooks/{author}/{genre}")]
        [HttpGet]
        public async Task<IActionResult> getBooksByAuthorAndGenre(string author, string genre)
        {
            if (!allowedGenre.Contains(genre))
            {
                return BadRequest();
            }

            List<Book> book = await _context.Book.Where(book => book.Author == author && book.Genre == genre).ToListAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Route("addBook")]
        [HttpPost]
        public async Task<IActionResult> addBook([FromBody]Book book)
        {
            if (!allowedGenre.Contains(book.Genre))
            {
                return BadRequest();
            }

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return Ok("Book " + book.Name + " added !");
        }

        [Route("editBook/{id}")]
        [HttpPut]
        public async Task<IActionResult> editBook(long id, [FromBody]Book book)
        {
            if (id != book.Id || !allowedGenre.Contains(book.Genre))
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Book " + book.Name + " updated !"); 
        }

        [Route("deleteBook/{id}")]
        [HttpDelete]
        public async Task<IActionResult> deleteBook(long id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Book " + book.Name + " deleted !");
        }

    }
}
