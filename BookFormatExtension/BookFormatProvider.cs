using System;
using System.Globalization;
using System.Linq;
using Books;
using Books.Interfaces;

namespace BookFormatExtension
{
    /// <summary>
    /// Custom format provider
    /// </summary>
    public class BookFormatProvider : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;

        private ILog logger;

        public BookFormatProvider(ILog logger) : this(CultureInfo.CurrentCulture, logger) { }

        public BookFormatProvider(IFormatProvider parent, ILog logger)
        {
            this.parent = parent;

            this.logger = logger;
        }

        public string Format(string format, object argument, IFormatProvider formatProvider)
        {
            if (argument == null)
            {
                return String.Empty;
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            if (argument.GetType() != typeof(Book))
            {
                try
                {
                    return HandleOtherFormats(format, argument);
                }
                catch (FormatException error)
                {
                    logger.WriteInfo($"The format of {nameof(argument)} is invalid.");

                    logger.WriteFatal(error.StackTrace);

                    throw new FormatException($"The format of {nameof(argument)} is invalid.");
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
                    logger.WriteInfo($"The format of {nameof(format)} is invalid.");

                    logger.WriteFatal(error.StackTrace);

                    throw new FormatException($"The format of '{nameof(format)}' is invalid.");
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

            if(resultType != null) return ((IFormattable)argument).ToString(format, parent);

            return argument.ToString();
        }
    }
}
