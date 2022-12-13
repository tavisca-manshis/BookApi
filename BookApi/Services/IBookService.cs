using BookApi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public interface IBookService
    {
        BookListResponse getBooks();
        Book getBookById(string id);
        List<Book> getBooksByAuthorAndGenre(string author, string genre);
        BookResponse addBook(Book book);
        BookResponse editBook(string id, Book book);
        BookResponse deleteBook(Book book);
        BookResponse translateToBookResponse(Book book);
    }
}
