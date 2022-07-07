using BookApi.Contracts;
using BookApi.Data;
using BookApi.Validators;
using FluentValidation;
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
        public BookListResponse getBooks()
        {
            BookListResponse response = new BookListResponse();
            try
            {
                response.BookList = _context.Book.ToList();
            } catch(Exception e)
            {
                response.Errors.Add(e.Message);
            }
            return response;
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
            try
            {
                bookResponse = translateToBookResponse(book);

                var validatedRequest = _addBookRequestValidator.Validate(book);
                if (validatedRequest.IsValid)
                {
                    Guid guid = Guid.NewGuid();
                    book.Id = guid.ToString();
                    _context.Book.Add(book);
                    _context.SaveChanges();
                }
                else
                {
                    bookResponse = new BookResponse();
                    foreach (var error in validatedRequest.Errors)
                    {
                        bookResponse.Errors.Add(error.ErrorMessage);
                    }
                }
            } catch (Exception e)
            {
                bookResponse.Errors.Add(e.Message);
            }
            return bookResponse;

        }

        public BookResponse editBook(string id, Book book)
        {
            BookResponse bookResponse = null;
            try
            {
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
            } catch (Exception e)
            {
                bookResponse.Errors.Add(e.Message);

            }
            return bookResponse;
        }

        public BookResponse deleteBook(Book book)
        {
            BookResponse bookResponse = new BookResponse();
            try
            {
                _context.Book.Remove(book);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                bookResponse.Errors.Add(e.Message);

            }
            return bookResponse;
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
