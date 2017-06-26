using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookDatabase.Tests
{
    [TestClass]
    public class BookDatabaseFactoryTest
    {
        [TestMethod]
        public void TestBookDatabaseInstance()
        {
            var bookDatabase = BookDatabaseFactory.CreateDatabase();
            Assert.IsInstanceOfType(bookDatabase, typeof(BookDatabase));
        }
    }
}
