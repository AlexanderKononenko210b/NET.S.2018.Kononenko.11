using System.Collections.Generic;

namespace Books.Comparers
{
    /// <summary>
    /// Compare two books by lenght ther names
    /// </summary>
    public class CompareNameLenghtBook : IComparer<Book>
    {
        public int Compare(Book bookLeft, Book bookRight)
        {
            if (bookLeft == null && bookRight == null)
                return 0;
            if (bookLeft == null)
                return -1;
            if (bookRight == null)
                return 1;
            return bookRight.Name.Length - bookLeft.Name.Length;
        }
    }
}
