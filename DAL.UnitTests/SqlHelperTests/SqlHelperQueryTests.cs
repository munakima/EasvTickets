using NUnit.Framework;
using DAL.Core.Models;
using DAL.Core;

namespace DAL.UnitTests.SqlHelperTests
{
    [TestFixture]
    class SqlHelperQueryTests
    {
        [SetUp]
        public void SetUp() { }

        [TearDown]
        public void TearDown() { }

        #region Get Query
        [Test]
        public void GetQuery_ReturnValue_ContainsEvent()
        {
            //Arrange
            var expected = "SELECT * FROM [Event] WHERE [Event].Id = @Id";

            //Act 
            var actual = SqlHelper.GetQuery<Event>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetQuery_ReturnValue_ContainsStudent()
        {
            //Arrange
            var expected = "SELECT * FROM [Student] WHERE [Student].Id = @Id";

            //Act 
            var actual = SqlHelper.GetQuery<Student>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetQuery_ReturnValue_ContainsTicket()
        {
            //Arrange
            var expected = "SELECT * FROM [Ticket] WHERE [Ticket].Id = @Id";

            //Act 
            var actual = SqlHelper.GetQuery<Ticket>();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Create Query
        [Test]
        public void CreateQuery_ReturnValue_ContainsEvent()
        {
            //Arrange
            var expected = "INSERT INTO [Event] VALUES (@Name,@Description,@Price,@StudentPrice,@Date,@Place);";

            //Act
            var actual = SqlHelper.CreateQuery<Event>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateQuery_ReturnValue_ContainsTicket()
        {
            //Arrange
            var expected = "INSERT INTO [Ticket] VALUES (@EventId,@IsUsed,@StudentId);";

            //Act
            var actual = SqlHelper.CreateQuery<Ticket>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateQuery_ReturnValue_ContainsStudent()
        {
            //Arrange
            var expected = "INSERT INTO [Student] VALUES (@Email);";

            //Act
            var actual = SqlHelper.CreateQuery<Student>();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Update Query
        [Test]
        public void UpdateQuery_ReturnValue_ContainsEvent()
        {
            //Arrange
            var expected = "UPDATE [Event] SET Name=@Name,Description=@Description,Price=@Price,StudentPrice=@StudentPrice,Date=@Date,Place=@Place WHERE Id=@Id;SELECT * FROM [Event] WHERE Id=@Id;";

            //Act
            var actual = SqlHelper.UpdateQuery<Event>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateQuery_ReturnValue_ContainsStudent()
        {
            //Arrange
            var expected = "UPDATE [Student] SET Email=@Email WHERE Id=@Id;SELECT * FROM [Student] WHERE Id=@Id;";

            //Act
            var actual = SqlHelper.UpdateQuery<Student>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateQuery_ReturnValue_ContainsTicket()
        {
            //Arrange
            var expected = "UPDATE [Ticket] SET EventId=@EventId,IsUsed=@IsUsed,StudentId=@StudentId WHERE Id=@Id;SELECT * FROM [Ticket] WHERE Id=@Id;";

            //Act
            var actual = SqlHelper.UpdateQuery<Ticket>();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Delete Query
        [Test]
        public void DeleteQuery_ReturnValue_ContainsEvent()
        {
            //Arrange
            var expected = "DELETE FROM [Event] WHERE Id=@Id";

            //Act
            var actual = SqlHelper.DeleteQuery<Event>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteQuery_ReturnValue_ContainsStudent()
        {
            //Arrange
            var expected = "DELETE FROM [Student] WHERE Id=@Id";

            //Act
            var actual = SqlHelper.DeleteQuery<Student>();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteQuery_ReturnValue_ContainsTicket()
        {
            //Arrange
            var expected = "DELETE FROM [Ticket] WHERE Id=@Id";

            //Act
            var actual = SqlHelper.DeleteQuery<Ticket>();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
