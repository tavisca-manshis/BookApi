using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Contracts;
using Microsoft.EntityFrameworkCore;
using BookApi.Services;
using BookApi.Constants;

namespace BookApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public List<Book> getBooks()
        {
            return _bookService.getBooks();
        }

        [Route(KeyStore.ApiRoute.GetBook)]
        [HttpGet]
        public IActionResult getBookById(string id)
        {
            var book = _bookService.getBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Route(KeyStore.ApiRoute.GetCustomBooks)]
        [HttpGet]
        public IActionResult getBooksByAuthorAndGenre(string author, string genre)
        {
            if (!_bookService.isValidGenre(genre))
            {
                return BadRequest();
            }

            var books = _bookService.getBooksByAuthorAndGenre(author, genre);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [Route(KeyStore.ApiRoute.AddBook)]
        [HttpPost]
        public IActionResult addBook([FromBody]Book book)
        {
            if (!_bookService.isValidGenre(book.Genre))
            {
                return BadRequest();
            }

            _bookService.addBook(book);

            return Ok("Book " + book.Name + " added !");
        }

        [Route("editBook/{id}")]
        [HttpPut]
        public IActionResult editBook(string id, [FromBody]Book book)
        {
            if (id != book.Id || !_bookService.isValidGenre(book.Genre))
            {
                return BadRequest();
            }
            _bookService.editBook(id, book);
            return Ok("Book " + book.Name + " updated !"); 
        }

        [Route(KeyStore.ApiRoute.DeleteBook)]
        [HttpDelete]
        public IActionResult deleteBook(string id)
        {
            Book book = _bookService.getBookById(id);

            if (book == null)
            {
                return NotFound();
            }
            _bookService.deleteBook(book);
            return Ok("Book " + book.Name + " deleted !");
        }

    }
}
