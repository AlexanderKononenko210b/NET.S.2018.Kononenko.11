using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books;

namespace BookFormatExtension
{
    /// <summary>
    /// Custom format provider
    /// </summary>
    public class BookFormatProvider : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;

        public BookFormatProvider() : this(CultureInfo.CurrentCulture) { }

        public BookFormatProvider(IFormatProvider parent)
        {
            this.parent = parent;
        }

        public string Format(string format, object argument, IFormatProvider formatProvider)
        {
            if (argument == null)
            {
                return String.Empty;
            }

            if (formatProvider == null)
            {
                throw new ArgumentNullException($"Argument`s {nameof(formatProvider)} is null");
            }

            if (argument.GetType() != typeof(Book))
            {
                try
                {
                    return HandleOtherFormats(format, argument);
                }
                catch (FormatException error)
                {
                    throw new FormatException($"The format of {nameof(format)} is invalid.");
                }
            }

            var arraySupportFormats = new string[2] {"R", "S"};

            if (string.IsNullOrWhiteSpace(format) || arraySupportFormats.Contains(format.ToUpper(CultureInfo.InvariantCulture)) != true)
            {
                try
                {
                    return HandleOtherFormats(format, argument);
                }
                catch (FormatException error)
                {
                    throw new FormatException($"The format of '{format}' is invalid.");
                }
            }

            var book = argument as Book;

            if (book != null)
            {
                switch (format.ToUpperInvariant())
                {
                    case "G":
                        return book.ToString();

                    case "R":
                        return $"/ {book.ISBN}, / {book.Autor}, / {book.Name}, / {book.Publish}," +
                               $" / {book.Year}, / {book.CountPage}, / {book.Price}";

                    case "S":
                        return $"{book.ISBN}";

                    default:
                        throw new FormatException($"The {format} format string is not supported");
                }
            }

            return argument.ToString();
        }

        public object GetFormat(Type formatType)
        {
            if (formatType != typeof(ICustomFormatter))
                return null;
            return this;
        }

        /// <summary>
        /// Try support other format
        /// </summary>
        /// <param name="format">format in string performance</param>
        /// <param name="argument"></param>
        /// <returns></returns>
        private string HandleOtherFormats(string format, object argument)
        {
            var resultType = argument as IFormattable;

            if(resultType != null) return ((IFormattable)argument).ToString(format, CultureInfo.CurrentCulture);

            return argument.ToString();
        }
    }
}
