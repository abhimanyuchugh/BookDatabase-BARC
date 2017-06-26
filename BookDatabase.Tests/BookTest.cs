using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BookDatabase.Tests
{
    [TestClass]
    public class BookTest
    {
        [TestMethod]
        public void TestDeleteAuthorThatDoesntExist()
        {
            var authors = new List<Author>()
            {
                new Author("AuthorA"),
                new Author("AuthorB"),
            };
            var book = new Book("BookTitle", authors);
            book.DeleteAuthor(authors[0].Name + "1111");
            Assert.AreEqual(2, book.Authors.Count);
            CollectionAssert.AreEquivalent(authors, book.Authors.ToList());
        }

        [TestMethod]
        public void TestDeleteAuthor()
        {
            var authorA = new Author("AuthorA");
            var authorB = new Author("AuthorB");
            var authors = new List<Author>()
            {
                authorA,
                authorB,
            };
            var book = new Book("BookTitle", authors);
            book.DeleteAuthor(authorA.Name);
            Assert.AreEqual(1, book.Authors.Count);
            var expectedAuthors = new List<Author> { authorB };
            CollectionAssert.AreEquivalent(expectedAuthors, book.Authors.ToList());
        }
    }
}
