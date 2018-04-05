using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Interface for predicate search book
    /// </summary>
    public interface IFindBook
    {
        bool IsFindBook(Book book);
    }
}
