using System;
using System.Globalization;
using NUnit.Framework;
using Books;
using BookFormatExtension;
using Books.Interfaces;
using Books.Loggers;
using Books.Storages;
using Books.Services;

namespace BooksAndBooksFormatExtension.Test
{
    /// <summary>
    /// Test performance instance type Book in various format`s
    /// </summary>
    [TestFixture]
    public class BooksAndBooksFormatExtensionTest
    {
        private readonly ILog logger = new NLogLogger();

        /// <summary>
        /// Test implement interface IFormattable class Book
        /// </summary>
        /// <param name="format">format in string performance</param>
        /// <param name="result">result string</param>
        [TestCase("G", "ISBN : 978-0-7356-6745-8, Autor : Astakhov Mihail, Name : Axperimental study of the strength, Publish : Bauman MSTU, Year : 2006, CountPage : 593, Price : ₹ 10.30")]
        [TestCase("O", "Astakhov Mihail,Axperimental study of the strength")]
        [TestCase("P", "Astakhov Mihail, Axperimental study of the strength, Bauman MSTU, 2006")]
        [TestCase("Q", "978-0-7356-6745-8, Astakhov Mihail, Axperimental study of the strength, Bauman MSTU, 2006")]
        public void Implement_Interface_IFormattable_Class_Book(string format, string result)
        {
            var storage = new BinaryStorage();

            var service = new BookListService(logger);

            service.ReadList(storage);

            var assert1 = service.ListBooks[0].ToString(format, CultureInfo.GetCultureInfo("en-IN"));

            Assert.AreEqual(result, assert1);
        }

        /// <summary>
        /// Test implement extension interface IFormatProvider ICustomFormatter where using dll
        /// </summary>
        /// <param name="format">format in string performance</param>
        /// <param name="result">result string</param>
        [TestCase("{0:G}", "ISBN : 978-0-7356-6745-8, Autor : Astakhov Mihail, Name : Axperimental study of the strength, Publish : Bauman MSTU, Year : 2006, CountPage : 593, Price : ₹ 10.30")]
        [TestCase("{0:R}", "/ 978-0-7356-6745-8, / Astakhov Mihail, / Axperimental study of the strength, / Bauman MSTU, / 2006, / 593, / 10,3")]
        [TestCase("{0:S}", "978-0-7356-6745-8")]
        [TestCase("{0:O}", "Astakhov Mihail,Axperimental study of the strength")]
        [TestCase("{0:P}", "Astakhov Mihail, Axperimental study of the strength, Bauman MSTU, 2006")]
        [TestCase("{0:Q}", "978-0-7356-6745-8, Astakhov Mihail, Axperimental study of the strength, Bauman MSTU, 2006")]
        public void Implement_Interface_IFormatProvider_ICustomFormatter_Class_Book(string format, string result)
        {
            var storage = new BinaryStorage();

            var service = new BookListService(logger);

            service.ReadList(storage);

            var customFormat = new BookFormatProvider(logger);

            var assert1 = String.Format(customFormat, format, service.ListBooks[0]);

            Assert.AreEqual(result, assert1);
        }

        /// <summary>
        /// Test implement extension interface IFormatProvider ICustomFormatter if expected FormatException
        /// </summary>
        /// <param name="format">format in string performance</param>
        /// <param name="result">result string</param>
        [TestCase("{0:Z}", "978-0-7356-6745-8")]
        public void Extension_Class_Book_Expected_FormatException_If_Format_Is_Not_Support(string format, string result)
        {
            var storage = new BinaryStorage();

            var service = new BookListService(logger);

            service.ReadList(storage);

            var customFormat = new BookFormatProvider(logger);

            Assert.Throws<FormatException>(() => String.Format(customFormat, format, service.ListBooks[0]));
        }
    }
}
