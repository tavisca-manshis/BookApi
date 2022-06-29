using BookApi.Contracts;
using BookApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class BookService: IBookService
    {

        private readonly BookContext _context;
        string[] allowedGenre = { "SciFi", "Horror", "Romance" };
        public BookService(BookContext bookContext)
        {
            _context = bookContext;
        }
        public List<Book> getBooks()
        {
            return _context.Book.ToList();
        }

        public Book getBookById(string id)
        {
            return _context.Book.Find(id);
        }

        public List<Book> getBooksByAuthorAndGenre(string author, string genre)
        {
            return _context.Book.Where(book => book.Author == author && book.Genre == genre).ToList();
        }

        public bool isValidGenre(string genre)
        {
            return allowedGenre.Contains(genre);
        }

        public void addBook(Book book)
        {
            Guid guid = Guid.NewGuid();
            book.Id = guid.ToString();
            _context.Book.Add(book);
            _context.SaveChanges();
        }

        public void editBook(string id, Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void deleteBook(Book book)
        {
            _context.Book.Remove(book);
            _context.SaveChanges();
        }
    }
}
