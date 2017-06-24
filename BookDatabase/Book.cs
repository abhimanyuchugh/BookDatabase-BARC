using System.Collections.Generic;

namespace BookDatabase
{
    public class Book
    {
        public Book(string title, IReadOnlyCollection<Author> authors)
        {
            Title = title;
            Authors = authors;
        }

        public string Title { get; private set; }
        public IReadOnlyCollection<Author> Authors { get; private set; }
    }
}
