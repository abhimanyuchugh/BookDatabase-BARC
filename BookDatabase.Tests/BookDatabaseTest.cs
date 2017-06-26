using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BookDatabase.Tests
{
    [TestClass]
    public class BookDatabaseTest
    {
        private BookDatabase bookDatabase;

        [TestInitialize]
        public void SetUp()
        {
            bookDatabase = new BookDatabase();
        }

        [TestMethod]
        public void TestInitialiseWithBooks()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            var bookDatabase = new BookDatabase(new List<Book>() { book });
            var bookAuthors = bookDatabase.GetAuthorsForBook(book.Title);
            Assert.AreEqual(2, bookAuthors.Count);
            var bookTitles = bookDatabase.GetBookTitlesForAuthor(authors[0].Name);
            Assert.AreEqual(1, bookTitles.Count);
            bookTitles = bookDatabase.GetBookTitlesForAuthor(authors[1].Name);
            Assert.AreEqual(1, bookTitles.Count);
        }

        [TestMethod]
        public void TestAddBookNull()
        {
            try
            {
                bookDatabase.AddBook(null);
                Assert.Fail("Trying to add a null book should fail");
            }
            catch (ArgumentNullException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentNullException");
            }
        }

        [TestMethod]
        public void TestAddBookSuccess()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            try
            {
                bookDatabase.AddBook(book);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestAddBookAlreadyExists()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase.AddBook(book);
            try
            {
                var bookCopy = new Book(book.Title, authors);
                bookDatabase.AddBook(bookCopy);
                Assert.Fail("Add book operation should fail when adding a book title that already exists");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentNullException");
            }
        }

        [TestMethod]
        public void TestDeleteBookSuccess()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                bookDatabase.DeleteBook(book.Title);
            }
            catch(Exception e)
            {
                Assert.Fail();
            }
            // Check book title
            try
            {
                bookDatabase.GetAuthorsForBook(book.Title);
                Assert.Fail("Book should not exist anymore");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Should throw ArgumentException");
            }
            // Check authors
            try
            {
                bookDatabase.GetBookTitlesForAuthor(authors[0].Name);
                Assert.Fail("Book should not exist anymore");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Should throw ArgumentException");
            }
        }

        [TestMethod]
        public void TestDeleteBookNull()
        {
            try
            {
                bookDatabase.DeleteBook(null);
                Assert.Fail("Deleting null book should fail");
            }
            catch (ArgumentNullException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentNullException");
            }
        }

        [TestMethod]
        public void TestDeleteBookThatDoesntExist()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                bookDatabase.DeleteBook(book.Title + "1111");
            }
            catch (Exception e)
            {
                Assert.Fail("Delete book operation should not fail when deleting a non-existent book");
            }
        }

        [TestMethod]
        public void TestDeleteBooksByAuthorSuccess()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            var authorName = authors[0].Name;
            try
            {
                bookDatabase.DeleteBooksByAuthor(authorName, false);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            // Check book title
            try
            {
                var bookAuthors = bookDatabase.GetAuthorsForBook(book.Title);
                Assert.IsFalse(bookAuthors.Contains(authorName));
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            // Check authors
            try
            {
                bookDatabase.GetBookTitlesForAuthor(authorName);
                Assert.Fail("Author should not exist anymore");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Should throw ArgumentException");
            }
        }

        [TestMethod]
        public void TestDeleteBooksByAuthorSuccess_Multi()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                bookDatabase.DeleteBooksByAuthor(authors[0].Name, true);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            // Check book title
            try
            {
                bookDatabase.GetAuthorsForBook(book.Title);
                Assert.Fail("Book should not exist anymore");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Should throw ArgumentException");
            }
            // Check authors
            try
            {
                bookDatabase.GetBookTitlesForAuthor(authors[0].Name);
                Assert.Fail("Book should not exist anymore");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception)
            {
                Assert.Fail("Should throw ArgumentException");
            }
        }

        [TestMethod]
        public void TestDeleteBooksByAuthorNull()
        {
            TestDeleteBooksByAuthorNull(true);
            TestDeleteBooksByAuthorNull(false);
        }

        private void TestDeleteBooksByAuthorNull(bool deleteMultiAuthoredBooks)
        {
            try
            {
                bookDatabase.DeleteBooksByAuthor(null, deleteMultiAuthoredBooks);
                Assert.Fail("Deleting books by null author should fail");
            }
            catch (ArgumentNullException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentNullException");
            }
        }


        [TestMethod]
        public void TestDeleteBooksByAuthorThatDoesntExist()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                bookDatabase.DeleteBooksByAuthor(authors[0].Name + "1111", true);
                bookDatabase.DeleteBooksByAuthor(authors[0].Name + "1111", false);
            }
            catch (Exception e)
            {
                Assert.Fail("Delete books by author operation should not fail when deleting those by an unknown author");
            }
        }

        [TestMethod]
        public void TestGetAuthorsForBook()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                var bookAuthors = bookDatabase.GetAuthorsForBook(book.Title);
                Assert.AreEqual(2, bookAuthors.Count);
                var authorNames = authors.Select(a => a.Name).ToList();
                CollectionAssert.AreEquivalent(authorNames, bookAuthors);
            }
            catch (Exception e)
            {
                Assert.Fail("Getting authors should not fail when book exists");
            }
        }

        [TestMethod]
        public void TestGetAuthorsForBookNull()
        {
            try
            {
                bookDatabase.GetAuthorsForBook(null);
                Assert.Fail("Getting authors for null book should fail");
            }
            catch (ArgumentNullException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentNullException");
            }
        }

        [TestMethod]
        public void TestGetAuthorsForBookThatDoesntExist()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                bookDatabase.GetAuthorsForBook(book.Title + "1111");
                Assert.Fail("Should throw exception if the book title doesn't exist");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentException");
            }
        }

        [TestMethod]
        public void TestGetBookTitlesForAuthor()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book1 = new Book("BookTitle1", authors);
            var book2 = new Book("BookTitle2", new List<Author>() { authors[0] });
            var books = new List<Book>() { book1, book2 };
            bookDatabase = new BookDatabase(books);
            try
            {
                var bookTitles = bookDatabase.GetBookTitlesForAuthor(authors[0].Name);
                Assert.AreEqual(2, bookTitles.Count);
                var titles = books.Select(a => a.Title).ToList();
                CollectionAssert.AreEquivalent(titles, bookTitles);
            }
            catch (Exception e)
            {
                Assert.Fail("Getting book titles for author should not fail when author exists");
            }
        }

        [TestMethod]
        public void TestGetBookTitlesForAuthorNull()
        {
            try
            {
                bookDatabase.GetBookTitlesForAuthor(null);
                Assert.Fail("Getting book titles for null author should fail");
            }
            catch (ArgumentNullException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentNullException");
            }
        }

        [TestMethod]
        public void TestGetBookTitlesForAuthorThatDoesntExist()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase = new BookDatabase(new List<Book>() { book });
            try
            {
                bookDatabase.GetBookTitlesForAuthor(authors[0].Name + "1111");
                Assert.Fail("Should throw exception when the author doesn't exist");
            }
            catch (ArgumentException)
            {
            }
            catch (Exception e)
            {
                Assert.Fail("Should throw ArgumentException");
            }
        }
    }
}
