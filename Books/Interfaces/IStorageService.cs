using System.Collections.Generic;

namespace Books.Interfaces
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
