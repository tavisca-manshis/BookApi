using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Contracts
{
    public class BookListResponse
    {
        public BookListResponse()
        {
            Errors = new List<string>();
            BookList = new List<Book>();
        }
        public List<Book> BookList { get; set; }
        public List<string> Errors { get; set; }
    }
}
