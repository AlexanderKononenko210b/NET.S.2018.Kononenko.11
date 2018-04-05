using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Service which work with List of Book
    /// </summary>
    public class BookListService
    {
        #region Field and Properties

        private IStorageService<List<Book>> storageService;

        private List<Book> listBooks = new List<Book>();

        public List<Book> ListBooks => listBooks;

        #endregion Field and Properties

        #region Constructor

        /// <summary>
        /// Constructor BookListService
        /// </summary>
        /// <param name="serviceStorage">instance type IStorageService</param>
        public BookListService(IStorageService<List<Book>> serviceStorage)
        {
            this.storageService = serviceStorage;

            listBooks = this.storageService.GetList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method for add new Book
        /// </summary>
        /// <param name="book">new book</param>
        /// <returns>object that save in storage</returns>
        public Book AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"Argument {nameof(listBooks)} is null");
            }

            var predicateByIsbn = new SearchByIsbn(book.ISBN);

            if (this.FindBookByTag(predicateByIsbn) != null)
            {
                throw new ExistInStorageException($"Book already exist in List");
            }

            listBooks.Add(book);

            if (this.FindBookByTag(predicateByIsbn) == null)
            {
                throw new InvalidOperationException($"Operation add is not dune");
            }

            return book;
        }

        /// <summary>
        /// Method for remove book from storage
        /// </summary>
        /// <param name="book">object book for remove</param>
        /// <returns>object that remove from storage</returns>
        public Book RemoveBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"Argument {nameof(listBooks)} is null");
            }

            var predicateByIsbn = new SearchByIsbn(book.ISBN);

            if (this.FindBookByTag(predicateByIsbn) == null)
            {
                throw new ExistInStorageException($"Book does not exist in List");
            }

            listBooks.Remove(book);

            if (this.FindBookByTag(predicateByIsbn) != null)
            {
                throw new InvalidOperationException($"Operation delete is not dune");
            }

            return book;
        }

        /// <summary>
        /// Method for find book till predicate
        /// </summary>
        /// <param name="tag">predicate for search</param>
        /// <returns>find object</returns>
        public Book FindBookByTag(IFindBook tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException($"Argument {nameof(tag)} is null");
            }

            foreach (Book book in listBooks)
            {
                if (tag.IsFindBook(book))
                    return book;
            }

            return null;
        }

        /// <summary>
        /// Method for sort List of Books
        /// </summary>
        /// <param name="tag"> list instans Book</param>
        /// <returns></returns>
        public void SortBooksByTag(ICompareBook tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException($"Argument {nameof(tag)} is null");
            }
            var countIteration = listBooks.Count;

            var compare = new CompareByName();

            while (countIteration != 1)
            {
                for (int i = 0; i < countIteration - 1; i++)
                {
                    var resultCompare = compare.Compare(listBooks[i], listBooks[i + 1]);

                    if (resultCompare > 0)
                    {
                        Swap(i, i + 1);
                    }
                }

                countIteration--;
            }
        }

        /// <summary>
        /// Helper method replace instance type Book in List
        /// </summary>
        /// <param name="indexLeft">first instance book</param>
        /// <param name="indexRight">second instance book</param>
        private void Swap(int indexLeft, int indexRight)
        {
            var helper = listBooks[indexLeft];

            listBooks[indexLeft] = listBooks[indexRight];

            listBooks[indexRight] = helper;
        }

        /// <summary>
        /// Method for save list Books
        /// </summary>
        public void SaveList()
        {
            storageService.SaveList(listBooks);
        }

        #endregion Methods
    }
}
