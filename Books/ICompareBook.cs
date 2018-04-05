using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Interface for compare two books
    /// </summary>
    public interface ICompareBook
    {
        int Compare(Book first, Book second);
    }
}
