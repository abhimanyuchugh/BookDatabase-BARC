using System.Collections.Generic;

namespace BookDatabase
{
    /// <summary>
    /// Provides an interface for interacting with the book database
    /// </summary>
    public interface IBookDatabase
    {
        /// <summary>
        /// Add the following book to the database
        /// </summary>
        /// <param name="book"></param>
        void AddBook(Book book);

        /// <summary>
        /// Delete the book with the given title
        /// </summary>
        /// <param name="bookTitle"></param>
        void DeleteBook(string bookTitle);

        /// <summary>
        /// Delete all books by the given author
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="deleteMultiAuthoredBooks">Delete books written by multiple authors (incl. the given author), instead of just removing the given author's name</param>
        void DeleteBooksByAuthor(string authorName, bool deleteMultiAuthoredBooks);

        /// <summary>
        /// Get all book titles for the given author
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        List<string> GetBookTitlesForAuthor(string authorName);

        /// <summary>
        /// Get the names of all authors for the book with given title
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        List<string> GetAuthorsForBook(string bookTitle);
    }
}
