﻿namespace BookDatabase
{
    public class Author
    {
        public Author(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
