using Books.Interfaces;

namespace Books.Comparers
{
    /// <summary>
    /// Compare elements List by first Literal name book alphabet
    /// </summary>
    public class CompareByName : ICompareBook
    {
        public int Compare(Book first, Book second)
        {
            if (first == null && second == null) return 0;

            if (first == null) return -1;

            if (second == null) return 1;

            return first.Name[0] - second.Name[0];
        }
    }
}
