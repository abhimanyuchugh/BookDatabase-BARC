using System.Collections.Generic;

namespace BookDatabase
{
    /// <summary>
    /// Provides an interface for interacting with the book database
    /// </summary>
    public interface IBookDatabase
    {
        /// <summary>
        /// Add the following book to the database, returning the success/failure of the operation as the result
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Whether the addition was successful</returns>
        void AddBook(Book book);

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

        /// <summary>
        /// Delete the book with the given title
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns>Whether the deletion was successful</returns>
        void DeleteBook(string bookTitle);

        /// <summary>
        /// Delete all books by the given author
        /// NOTE: Debatable whether this should delete books where there are multiple authors
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns>Whether the deletion was successful</returns>
        void DeleteBooksByAuthor(string authorName);
    }
}
