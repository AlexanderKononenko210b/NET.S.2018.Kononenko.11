using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Books
{
    /// <summary>
    /// Class Book
    /// </summary>
    public class Book : IComparable<Book>, IEquatable<Book>, IFormattable
    {
        #region Fields

        private readonly string isbn;

        private string autor;

        private string name;

        private string publish;

        private int year;

        private int countpage;

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
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new BookFormatException($"Value {nameof(Autor)} is not entered!");
                }
                else
                {
                    string regex = @"^[A-Z]{1}[a-z]{1,15}[ ]{1}[A-Z]{1}[a-z]{1,15}";
                    if (!Regex.IsMatch(value, regex))
                    {
                        throw new BookFormatException($"Value {nameof(Autor)} is not correct! Example Jeffrey Richter");
                    }

                    autor = value;
                }
            }
        }

        /// <summary>
        /// Book`s name
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new BookFormatException($"Value {nameof(Name)} is not entered!");
                }
                else
                {
                    string regex = @".{3,50}";
                    if (!Regex.IsMatch(value, regex))
                    {
                        throw new BookFormatException($"Value {nameof(Name)} is not correct! Example CLR via C#");
                    }

                    name = value;
                }
            }
        }

        /// <summary>
        /// Publish`s name
        /// </summary>
        public string Publish
        {
            get => publish;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new BookFormatException($"Value {nameof(Publish)} is not entered!");
                }
                else
                {
                    string regex = @".{1,50}";
                    if (!Regex.IsMatch(value, regex))
                    {
                        throw new BookFormatException($"Value {nameof(Publish)} is not correct! Example Microsoft Press");
                    }

                    publish = value;
                }
            }
        }

        /// <summary>
        /// The year of publishing
        /// </summary>
        public int Year
        {
            get => year;
            set
            {
                if (value < 1900 || value > DateTime.Now.Year)
                {
                    throw new BookFormatException($"Value {nameof(Year)} is not correct! Thay can be more than 1900 and less {DateTime.Now.Year}");
                }

                year = value;
            }
        }

        /// <summary>
        /// Count pages
        /// </summary>
        public int CountPage
        {
            get => countpage;
            set
            {
                if (value < 0)
                {
                    throw new BookFormatException($"Value {nameof(CountPage)} is not correct! Thay can be more than 0");
                }

                countpage = value;
            }
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
            this.Autor = autor;
            this.Name = name;
            this.Publish = publish;
            this.Year = year;
            this.CountPage = count;
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

            if (!this.ISBN.Equals(book.ISBN)) return false;

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
            return this.ISBN.GetHashCode();
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
