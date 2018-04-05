using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books;
using NLog;

namespace ConsoleTest
{
    class Program
    {
        private static readonly ILogger logger = new NLogLogger();

        static void Main(string[] args)
        {
            try
            {
                var storage = new BinaryStorage();

                var service = new BookListService(storage);

                var book1 = new Book("978-0-7356-6745-7", "Jeffrey Richter",
                    "CLR via C#", "Microsoft Press", 2012, 826, new decimal(59.99));

                //service.AddBook(book1);

                var book2 = new Book("978-0-7356-6745-8", "Astakhov Mihail",
                    "Axperimental study of the strength",
                    "Bauman MSTU", 2006, 593, new decimal(10.30));

                //service.AddBook(book2);

                var book3 = new Book("978-0-7356-6745-1", "Timoshenko Sergey",
                    "Bibration problems in engineering",
                    "Mashinostroenie Publ", 1985, 472, new decimal(30.45));

                //service.AddBook(book3);

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine();

                service.RemoveBook(book3);

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine();

                var resultDuneFind = service.FindBookByTag(new SearchByIsbn(book2.ISBN));

                Console.WriteLine($"Expected true: {resultDuneFind.Equals(book2)}");

                var resultNotFind = service.FindBookByTag(new SearchByIsbn("978 - 0 - 7356 - 3745 - 7"));

                Console.WriteLine($"Expected null: {resultNotFind == null}");

                var predicateSort = new CompareByName();

                service.AddBook(book3);

                service.SortBooksByTag(predicateSort);

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine();

                storage.SaveList(service.ListBooks);

                var cultereInfo = System.Globalization.CultureInfo.GetCultureInfo("en-IN");

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString("O", cultereInfo));
                }

                Console.WriteLine();

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString("P", cultereInfo));
                }

                Console.WriteLine();

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString("Q", cultereInfo));
                }

                Console.WriteLine();

                foreach (Book item in service.ListBooks)
                {
                    Console.WriteLine(item.ToString("G", cultereInfo));
                }
            }
            catch (FormatException error)
            {
                logger.WriteInfo("Format exseption");
                logger.WriteError(error.StackTrace);
            }
            catch (ArgumentNullException error)
            {
                logger.WriteInfo("Argument null exception");
                logger.WriteError(error.StackTrace);
            }
            catch (BookFormatException error)
            {
                logger.WriteInfo("Book format exception");
                logger.WriteError(error.StackTrace);
            }
            catch (Exception error)
            {
                logger.WriteInfo("Unhandled exception:");
                logger.WriteError(error.StackTrace);
            }
            
        }
    }
}
