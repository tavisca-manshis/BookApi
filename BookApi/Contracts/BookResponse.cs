using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Contracts
{
    public class BookResponse : Book
    {
        public BookResponse()
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; set; }

    }
}
