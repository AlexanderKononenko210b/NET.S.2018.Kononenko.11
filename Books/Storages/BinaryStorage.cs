using System;
using System.Collections.Generic;
using System.IO;
using Books.Interfaces;
using System.Configuration;

namespace Books.Storages
{
    /// <summary>
    /// Binary storage for book`s list
    /// </summary>
    public class BinaryStorage : IStorageService<List<Book>>
    {
        string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["filePath"]);

        /// <summary>
        /// Method for get list of book
        /// </summary>
        /// <returns></returns>
        public List<Book> GetList()
        {
            List<Book> listBooks = new List<Book>(); 

            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"Current file is not exist in specified location");
            }

            using (BinaryReader binaryStream = new BinaryReader(File.Open(sourcePath, FileMode.Open, FileAccess.Read)))
            {
                if (binaryStream.PeekChar() != -1)
                {
                    while (binaryStream.PeekChar() != -1)
                    {
                        string isbn = binaryStream.ReadString();
                        string autor = binaryStream.ReadString();
                        string name = binaryStream.ReadString();
                        string publish = binaryStream.ReadString();
                        int year = binaryStream.ReadInt32();
                        int countPage = binaryStream.ReadInt32();
                        decimal price = binaryStream.ReadDecimal();

                        var book = new Book(isbn, autor, name, publish, year, countPage, price);

                        listBooks.Add(book);
                    }
                }
            }

            return listBooks;
        }

        /// <summary>
        /// Method for save book`s list in storage
        /// </summary>
        /// <param name="listBook"></param>
        public void SaveList(List<Book> listBook)
        {
            string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["filePath"]);

            if (listBook == null)
            {
                throw new ArgumentNullException($"Argument {nameof(listBook)} is null");
            }

            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"Current file is not exist in specified location");
            }

            using (BinaryWriter binaryStream = new BinaryWriter(File.Open(sourcePath, FileMode.Open, FileAccess.Write)))
            {
                foreach (Book item in listBook)
                {
                    binaryStream.Write(item.ISBN);
                    binaryStream.Write(item.Autor);
                    binaryStream.Write(item.Name);
                    binaryStream.Write(item.Publish);
                    binaryStream.Write(item.Year);
                    binaryStream.Write(item.CountPage);
                    binaryStream.Write(item.Price);
                }
            }
        }
    }
}
