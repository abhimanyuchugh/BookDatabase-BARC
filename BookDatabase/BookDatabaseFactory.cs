using System.Collections.Generic;

namespace BookDatabase
{
    /// <summary>
    /// Although not necessary for this simple use case, this could be converted into
    /// an Abstract Factory pattern, if more database types were added in the future
    /// whose initialisation requirements differed type by type
    /// </summary>
    public class BookDatabaseFactory
    {
        public static IBookDatabase CreateDatabase(List<Book> books = null)
        {
            return new BookDatabase(books);
        }
    }
}
