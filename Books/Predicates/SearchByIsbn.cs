using Books.Interfaces;

namespace Books.Predicates
{
    /// <summary>
    /// Class predicate for search book by ISBN number
    /// </summary>
    public class SearchByIsbn : IPredicate
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
