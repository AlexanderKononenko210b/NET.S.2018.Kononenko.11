using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Class predicate for search book by ISBN number
    /// </summary>
    public class SearchByIsbn : IFindBook
    {
        private readonly string isbn;

        public SearchByIsbn(string isbnInput)
        {
            this.isbn = isbnInput;
        }

        public bool IsFindBook(Book book)
        {
            if (!book.ISBN.Equals(this.isbn))
            {
                return false;
            }

            return true;
        }
    }
}
