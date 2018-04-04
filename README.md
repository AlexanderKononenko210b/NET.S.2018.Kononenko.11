# NET.S.2018.Kononenko.11

1. Develop a Book class (ISBN, author, title, publisher, year of publication, number of pages, price), redefining the necessary methods for the Object class. For class objects, implement equivalence and order relations (using the appropriate interfaces). To perform basic operations with the list of books that you can download and, if necessary, save in a certain BookListStorage repository to develop the BookListService class (as a service for working with the collection of books) with AddBook functionality (add a book if there is no such book, otherwise discard an exception); RemoveBook (delete the book, if it exists, otherwise throw an exception); FindBookByTag (find the book by the given criterion); SortBooksByTag (sort the list of books according to the specified criteria), do not use delegates when implementing them! The work of the classes is demonstrated on the example of the console application. As a repository, use
a binary file for use with only the BinaryReader, BinaryWriter classes. The repository can later be changed / added.

2. Implement the ability to log messages of various levels, providing the possibility of using different frameworks for logging.

3. For Book objects (ISBN, author, title, publisher, year of publication, number of pages, price) realize the possibility of a string representation of a different kind. For example, for an object with ISBN values ​​of 978-0-7356-6745-7, AuthorName = Jeffrey Richter, Title = CLR via C #, Publisher = Microsoft Press, Year = 2012, NumberOPpages = 826, Price = 59.99 $, there may be options:
Jeffrey Richter, CLR via C #
Jeffrey Richter, CLR via C #, "Microsoft Press", 2012
ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C #, "Microsoft Press", 2012, P. 826.
Jeffrey Richter, CLR via C #, "Microsoft Press", 2012
ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C #, "Microsoft Press", 2012, P. 826., 59.99 $. etc.

4. Without changing the Book class, add an additional formatting capability for objects of this class that was not originally provided by the class.

5. For those realized in pp. 3, 4 functionality to develop unit tests.
