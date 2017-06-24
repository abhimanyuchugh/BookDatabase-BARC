using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddBookNull()
        {
            bookDatabase.AddBook(null);
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
            bookDatabase.AddBook(book);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddBookAlreadyExists()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            bookDatabase.AddBook(book);
            bookDatabase.AddBook(book);
        }
    }
}
