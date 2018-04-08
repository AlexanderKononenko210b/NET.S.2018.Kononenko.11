using System;
using System.Collections.Generic;
using Books.Interfaces;
using Books.Exceptions;
using Books.Comparers;
using Books.Predicates;

namespace Books.Services
{
    /// <summary>
    /// Service which work with List of Book
    /// </summary>
    public class BookListService
    {
        #region Field and Properties

        private ILog logger;

        private List<Book> listBooks = new List<Book>();

        public List<Book> ListBooks => listBooks;

        #endregion Field and Properties

        #region Constructor

        /// <summary>
        /// Constructor BookListService
        /// </summary>
        /// <param name="logger">logger</param>
        public BookListService(ILog logger)
        {
            this.logger = logger;
        }

        #endregion Constructor

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
                logger.WriteInfo($"Argument {nameof(listBooks)} is null");

                throw new ArgumentNullException($"Argument {nameof(listBooks)} is null in method {nameof(AddBook)}");
            }

            var predicateByIsbn = new SearchByIsbn(book.ISBN);

            if (this.FindBookByTag(predicateByIsbn) != null)
            {
                logger.WriteInfo($"Book {nameof(book)} already exist in List in method {nameof(AddBook)}");

                throw new ExistInStorageException($"Book already exist in List");
            }

            listBooks.Add(book);

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
                logger.WriteInfo($"Argument {nameof(listBooks)} is null in method {nameof(RemoveBook)}");

                throw new ArgumentNullException($"Argument {nameof(listBooks)} is null");
            }

            var predicateByIsbn = new SearchByIsbn(book.ISBN);

            if (this.FindBookByTag(predicateByIsbn) == null)
            {
                logger.WriteInfo($"Book {nameof(book)} does not exist in List in method {nameof(RemoveBook)}");

                throw new ExistInStorageException($"Book {nameof(book)} does not exist in List");
            }

            listBooks.Remove(book);

            return book;
        }

        /// <summary>
        /// Method for find book till predicate
        /// </summary>
        /// <param name="tag">predicate for search</param>
        /// <returns>find object</returns>
        public Book FindBookByTag(IPredicate tag)
        {
            if (tag == null)
            {
                logger.WriteInfo($"Argument {nameof(tag)} is null in method {nameof(FindBookByTag)}");

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
                logger.WriteInfo($"Argument {nameof(tag)} is null in method {nameof(SortBooksByTag)}");

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
        /// Method for read list Books
        /// </summary>
        public void ReadList(IStorageService<List<Book>> storageService)
        {
            try
            {
                listBooks = storageService.GetList();
            }
            catch (Exception error)
            {
                logger.WriteInfo(error.Message);

                logger.WriteError(error.StackTrace);

                throw;
            }
        }

        /// <summary>
        /// Method for save list Books
        /// </summary>
        public void SaveList(IStorageService<List<Book>> storageService)
        {
            try
            {
                storageService.SaveList(listBooks);
            }
            catch(Exception error)
            {
                logger.WriteInfo(error.Message);

                logger.WriteError(error.StackTrace);

                throw;
            }
        }

        #endregion Methods
    }
}
