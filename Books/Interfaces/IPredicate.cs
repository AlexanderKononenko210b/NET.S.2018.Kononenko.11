
namespace Books.Interfaces
{
    /// <summary>
    /// Interface for predicate search book
    /// </summary>
    public interface IPredicate
    {
        bool IsFindBook(Book book);
    }
}
