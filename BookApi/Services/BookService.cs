using BookApi.Contracts;
using BookApi.Data;
using BookApi.Validators;
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
        private readonly AddBookRequestValidator _addBookRequestValidator;
        private readonly EditBookRequestValidator _editBookRequestValidator;
        public BookService(BookContext bookContext)
        {
            _context = bookContext;
            _addBookRequestValidator = new AddBookRequestValidator();
            _editBookRequestValidator = new EditBookRequestValidator();
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

        public BookResponse addBook(Book book)
        {
            BookResponse bookResponse = null;
            bookResponse = translateToBookResponse(book);

            var validatedRequest = _addBookRequestValidator.Validate(book);
            if (validatedRequest.IsValid)
            {
                Guid guid = Guid.NewGuid();
                book.Id = guid.ToString();
                _context.Book.Add(book);
                _context.SaveChanges();
            } else
            {
                bookResponse = new BookResponse();
                foreach(var error in validatedRequest.Errors)
                {
                    bookResponse.Errors.Add(error.ErrorMessage);
                }
            }
            return bookResponse;

        }

        public BookResponse editBook(string id, Book book)
        {
            BookResponse bookResponse = null;
            bookResponse = translateToBookResponse(book);

            var validatedRequest = _editBookRequestValidator.Validate(book);
            if (validatedRequest.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                foreach (var error in validatedRequest.Errors)
                {
                    bookResponse.Errors.Add(error.ErrorMessage);
                }
            }
            return bookResponse;
        }

        public void deleteBook(Book book)
        {
            _context.Book.Remove(book);
            _context.SaveChanges();
        }

        public BookResponse translateToBookResponse (Book book)
        {
            BookResponse bookResponse = new BookResponse();
            bookResponse.Name = book.Name;
            bookResponse.Author = book.Author;
            bookResponse.Genre = book.Genre;

            return bookResponse;
        }
    }
}
