using System;
using System.Collections.Generic;
using System.Linq;

namespace BookDatabase
{
    internal class BookDatabase : IBookDatabase
    {
        private readonly Dictionary<string, Book> bookTitleMap;
        private readonly Dictionary<string, List<Book>> authorBooksMap;

        public BookDatabase()
        {
            bookTitleMap = new Dictionary<string, Book>();
            authorBooksMap = new Dictionary<string, List<Book>>();
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentException("Book is null", "book");
            if (bookTitleMap.ContainsKey(book.Title))
                throw new ArgumentException("Book already exists", "book");
            bookTitleMap[book.Title] = book;
            if (book.Authors != null && book.Authors.Any())
            {
                foreach (var author in book.Authors)
                {
                    List<Book> authorBooks;
                    if (!authorBooksMap.TryGetValue(author.Name, out authorBooks))
                    {
                        authorBooks = new List<Book>();
                        authorBooksMap[author.Name] = authorBooks;
                    }
                    authorBooks.Add(book);
                }
            }
        }

        public void DeleteBook(string bookTitle)
        {
            Book book;
            if (!bookTitleMap.TryGetValue(bookTitle, out book))
                throw new ArgumentException("Invalid book title. Book does not exist.");
            bookTitleMap.Remove(bookTitle);
            if (book.Authors != null && book.Authors.Any())
            {
                foreach (var author in book.Authors)
                {
                    List<Book> authorBooks;
                    if (authorBooksMap.TryGetValue(author.Name, out authorBooks))
                    {
                        authorBooks.Remove(book);
                        if (!authorBooks.Any())
                        {
                            authorBooksMap.Remove(author.Name);
                        }
                    }
                }
            }
        }

        public void DeleteBooksByAuthor(string authorName)
        {
            List<Book> authorBooks;
            if (!authorBooksMap.TryGetValue(authorName, out authorBooks))
                throw new ArgumentException("Invalid author name. Author does not exist.");
            if (authorBooks != null && authorBooks.Any())
            {
                foreach (var book in authorBooks)
                {
                    DeleteBook(book.Title);
                }
            }
        }

        public List<string> GetAuthorsForBook(string bookTitle)
        {
            Book book;
            if (!bookTitleMap.TryGetValue(bookTitle, out book))
                throw new ArgumentException("Invalid book title. Book does not exist.");
            return book.Authors == null ? null : book.Authors.Select(a => a.Name).ToList();
        }

        public List<string> GetBookTitlesForAuthor(string authorName)
        {
            List<Book> books;
            if (!authorBooksMap.TryGetValue(authorName, out books))
                throw new ArgumentException("Invalid author name. Author does not exist.");
            return books == null ? null : books.Select(b => b.Title).ToList();
        }
    }
}
