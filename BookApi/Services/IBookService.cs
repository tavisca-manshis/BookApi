using BookApi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public interface IBookService
    {
        List<Book> getBooks();
        Book getBookById(string id);
        List<Book> getBooksByAuthorAndGenre(string author, string genre);
        void addBook(Book book);
        void editBook(string id, Book book);
        void deleteBook(Book book);
        bool isValidGenre(string genre);


    }
}
