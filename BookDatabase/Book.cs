using System.Collections.Generic;
using System.Linq;

namespace BookDatabase
{
    public class Book
    {
        /// <summary>
        /// Don't want to allow user to modify the authors list, hence private accessibility
        /// </summary>
        private readonly List<Author> authors;
        public Book(string title, List<Author> authors)
        {
            Title = title;
            this.authors = authors;
        }

        public string Title { get; private set; }
        public IReadOnlyCollection<Author> Authors
        {
            get
            {
                return authors.AsReadOnly();
            }
        }

        /// <summary>
        /// Delete reference to given author from book
        /// </summary>
        /// <param name="name"></param>
        internal void DeleteAuthor(string name)
        {
            if (authors == null || !authors.Any()) return;
            var author = authors.FirstOrDefault(a => a.Name.Equals(name));
            if (author != null)
                authors.Remove(author);
        }
    }
}
