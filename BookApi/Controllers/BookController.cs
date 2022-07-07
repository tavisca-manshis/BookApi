using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Contracts;
using Microsoft.EntityFrameworkCore;
using BookApi.Services;
using BookApi.Constants;
using BookApi.Filters;

namespace BookApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ResourceFilter()]
    [ExceptionFilter]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public BookListResponse getBooks()
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
            var books = _bookService.getBooksByAuthorAndGenre(author, genre);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [Route(KeyStore.ApiRoute.AddBook)]
        [HttpPost]
        [AuthorizationFilter]
        public IActionResult addBook([FromBody]Book book)
        {
            BookResponse response = _bookService.addBook(book);

            if (response == null || response.Errors.Count > 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Route("editBook/{id}")]
        [HttpPut]
        [AuthorizationFilter]
        public IActionResult editBook(string id, [FromBody]Book book)
        {
            BookResponse response = _bookService.editBook(id, book);
            if (response == null || response.Errors.Count > 0)
            {
                return BadRequest(response);
            }
            return Ok(response); 
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
            BookResponse response = _bookService.deleteBook(book);
            return Ok(response);
        }

    }
}
