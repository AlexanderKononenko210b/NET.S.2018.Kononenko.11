using System;
using System.Globalization;
using Books.Exceptions;

namespace Books
{
    /// <summary>
    /// Class Book
    /// </summary>
    public class Book : IComparable, IComparable<Book>, IEquatable<Book>, IFormattable
    {
        #region Fields

        private readonly string isbn;

        private readonly string autor;

        private readonly string name;

        private readonly string publish;

        private readonly int year;

        private readonly int countPage;

        private decimal price;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Number ISBN
        /// </summary>
        public string ISBN => isbn;

        /// <summary>
        /// Autor`s name and surname
        /// </summary>
        public string Autor
        {
            get => autor;
        }

        /// <summary>
        /// Book`s name
        /// </summary>
        public string Name
        {
            get => name;
        }

        /// <summary>
        /// Publish`s name
        /// </summary>
        public string Publish
        {
            get => publish;
        }

        /// <summary>
        /// The year of publishing
        /// </summary>
        public int Year
        {
            get => year;
        }

        /// <summary>
        /// Count pages
        /// </summary>
        public int CountPage
        {
            get => countPage;
        }

        /// <summary>
        /// Book`s price
        /// </summary>
        public decimal Price
        {
            get => price;
            set
            {
                if (value < 0)
                {
                    throw new BookFormatException($"Value {nameof(Price)} is not correct! Thay can be more than 0");
                }

                price = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor instance Book with parametr
        /// </summary>
        /// <param name="isbnNumber">value ISBN</param>
        /// <param name="autor">Autor`s name</param>
        /// <param name="name">Book`s name</param>
        /// <param name="publish">Publish`s name</param>
        /// <param name="year">Year book</param>
        /// <param name="count">Count page in book</param>
        /// <param name="price">Price of book</param>
        public Book(string isbnNumber, string autor, string name, string publish, int year, int count, decimal price)
        {
            this.isbn = isbnNumber;
            this.autor = autor;
            this.name = name;
            this.publish = publish;
            this.year = year;
            this.countPage = count;
            this.Price = price;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override method Equals for two Book
        /// </summary>
        /// <param name="book">second object for operation equals</param>
        /// <returns></returns>
        public bool Equals(Book book)
        {
            if (ReferenceEquals(null, book)) return false;

            if (ReferenceEquals(this, book)) return true;

            if (!(this.ISBN == book.ISBN && this.Autor == book.Autor
                && this.Name == book.Name && this.Publish == book.Publish
                && this.Year == book.Year && this.CountPage == book.CountPage)) return false;

            return true;
        }

        /// <summary>
        /// Override method Equals for two object
        /// </summary>
        /// <param name="obj">second object for operation equals</param>
        /// <returns>true if object equals</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return Equals((Book)obj);
        }

        /// <summary>
        /// Override GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ISBN.GetHashCode() + this.Autor.GetHashCode()
                + this.Name.GetHashCode() + this.Publish.GetHashCode() 
                + this.Year.GetHashCode() + this.CountPage.GetHashCode();
        }

        /// <summary>
        /// Override ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var specificCulture = System.Globalization.CultureInfo.GetCultureInfo("en-IN");

            return $"ISBN : {ISBN}, Autor : {Autor}, Name : {Name}, Publish : {Publish}," +
                   $" Year : {Year}, CountPage : {CountPage}, Price : {Price.ToString("C", specificCulture)}";
        }

        /// <summary>
        /// Implement method CompareTo interface IComparable
        /// </summary>
        /// <param name="obj">object for compare</param>
        /// <returns>result compare</returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return -1;

            if (this.Equals(obj)) return 0;

            var castBook = obj as Book;

            if ((obj as Book) == null)
            {
                throw new InvalidCastException($"Argument {nameof(obj)} does not cast in Book");
            }

            return this.GetNumberBook() - castBook.GetNumberBook();
        }

        /// <summary>
        /// Implement method CompareTo interface IComparable<paramref name="otherBook"/>
        /// </summary>
        /// <param name="otherBook">object for compare</param>
        /// <returns>result compare</returns>
        public int CompareTo(Book otherBook)
        {
            if (otherBook == null) return -1;

            if (this.Equals(otherBook)) return 0;

            return this.GetNumberBook() - otherBook.GetNumberBook();
        }

        /// <summary>
        /// Method for get number Book
        /// </summary>
        /// <returns>int performance number Book</returns>
        public int GetNumberBook()
        {
            var stringArray = this.ISBN.Split('-');

            if (!Int32.TryParse(stringArray[stringArray.Length - 2], out int i) ||
                !Int32.TryParse(stringArray[stringArray.Length - 1], out int j))
            {
                throw new BookFormatException($"Format ISBN is unknown");
            }

            return (int)((i + (float)j / 10) * 10);
        }

        /// <summary>
        /// Implement interface IFormattable
        /// </summary>
        /// <param name="format">string indicator format</param>
        /// <param name="formatProvider">provider format</param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format)) return "G";

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return this.ToString();

                case "O":
                    return $"{Autor},{Name}";

                case "P":
                    return $"{Autor}, {Name}, {Publish}, {Year}";

                case "Q":
                    return $"{ISBN}, {Autor}, {Name}, {Publish}, {Year}";

                default:
                    throw new FormatException($"The {format} format string is not supported");
            }
        }

        #endregion
    }
}
