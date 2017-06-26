using System;
using System.Collections.Generic;
using System.Linq;

namespace BookDatabase
{
    internal class BookDatabase : IBookDatabase
    {
        /// <summary>
        /// Book title mapped to their corresponding books
        /// </summary>
        private readonly Dictionary<string, Book> bookTitleMap;
        /// <summary>
        /// Author (names) mapped to their authored books
        /// </summary>
        private readonly Dictionary<string, List<Book>> authorBooksMap;

        /// <summary>
        /// Books constructor with optional initial population of the database
        /// </summary>
        /// <param name="books"></param>
        public BookDatabase(List<Book> books = null)
        {
            bookTitleMap = new Dictionary<string, Book>();
            authorBooksMap = new Dictionary<string, List<Book>>();
            if (books != null && books.Any())
            {
                foreach (var book in books)
                {
                    bookTitleMap[book.Title] = book;
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
        }

        /// <summary>
        /// Add the following book to the database
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book", "Book is null");
            if (bookTitleMap.ContainsKey(book.Title))
                throw new ArgumentException("Book already exists", "book");
            bookTitleMap[book.Title] = book;
            // add author mappings for this book
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

        /// <summary>
        /// Delete the book with the given title
        /// </summary>
        /// <param name="bookTitle"></param>
        public void DeleteBook(string bookTitle)
        {
            if (bookTitle == null)
                throw new ArgumentNullException("bookTitle", "Invalid book title. Book title cannot be null.");
            Book book;
            if (!bookTitleMap.TryGetValue(bookTitle, out book))
                return;
            // Remove book
            bookTitleMap.Remove(bookTitle);
            // Remove book reference from its authors' mappings
            if (book.Authors != null && book.Authors.Any())
            {
                foreach (var author in book.Authors)
                {
                    List<Book> authorBooks;
                    if (authorBooksMap.TryGetValue(author.Name, out authorBooks))
                    {
                        authorBooks.Remove(book);
                        // if this author has no other books left, remove the author completely
                        if (!authorBooks.Any())
                        {
                            authorBooksMap.Remove(author.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Delete all books by the given author
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="deleteMultiAuthoredBooks">Delete books written by multiple authors (incl. the given author), instead of just removing the given author's name</param>
        public void DeleteBooksByAuthor(string authorName, bool deleteMultiAuthoredBooks)
        {
            if (authorName == null)
                throw new ArgumentNullException("authorName", "Invalid author name. Author name cannot be null.");
            List<Book> authorBooks;
            if (!authorBooksMap.TryGetValue(authorName, out authorBooks))
                return;
            // Find all the books by this author and delete them one by one
            if (authorBooks == null || !authorBooks.Any()) return;
            // Create a new list since the delete operation might modify this list
            var authorBooksList = authorBooks.ToList();
            if (deleteMultiAuthoredBooks)
            {
                // if we should delete books even if they have other authors, just delete the book
                // which will delete the authors as well
                foreach (var book in authorBooksList)
                {
                    DeleteBook(book.Title);
                }
            }
            else
            {
                foreach (var book in authorBooksList)
                {
                    book.DeleteAuthor(authorName);
                    // if there are no other authors left, delete the book as well
                    if (!book.Authors.Any())
                        DeleteBook(book.Title);
                }
            }
            authorBooksMap.Remove(authorName);
        }

        /// <summary>
        /// Get the names of all authors for the book with given title
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        public List<string> GetAuthorsForBook(string bookTitle)
        {
            if (bookTitle == null)
                throw new ArgumentNullException("bookTitle", "Invalid book title. Book title cannot be null.");
            Book book;
            if (!bookTitleMap.TryGetValue(bookTitle, out book))
                throw new ArgumentException("Invalid book title. Book does not exist.");
            return book.Authors == null ? null : book.Authors.Select(a => a.Name).ToList();
        }

        /// <summary>
        /// Get all book titles for the given author
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        public List<string> GetBookTitlesForAuthor(string authorName)
        {
            if (authorName == null)
                throw new ArgumentNullException("authorName", "Invalid author name. Author name cannot be null.");
            List<Book> books;
            if (!authorBooksMap.TryGetValue(authorName, out books))
                throw new ArgumentException("Invalid author name. Author does not exist.");
            return books == null ? null : books.Select(b => b.Title).ToList();
        }
    }
}
