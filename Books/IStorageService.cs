using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Interface for storage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorageService<T> where T : List<Book>
    {
        T GetList();

        void SaveList(T list);
    }
}
