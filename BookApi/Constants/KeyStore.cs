using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Constants
{
    public class KeyStore
    {
        public static class ApiRoute
        {
            public const string GetBook = "getBook/{id}";
            public const string GetCustomBooks = "getBooks/{author}/{genre}";
            public const string AddBook = "addBook";
            public const string EditBook = "editBook/{id}";
            public const string DeleteBook = "deleteBook/{id}";
        }

        public static class UserRole
        {
            public const string Admin = "admin";
        }
    }
}
