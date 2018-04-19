using DAL.Core;
using NUnit.Framework;
using Moq;
using System.Data;
using System;
using System.Data.SqlClient;
using DAL.UnitTests.Helpers;
using DAL.Core.Models;

namespace DAL.UnitTests.SqlHelperTests
{
    [TestFixture]
    class SqlHelperBuilderTests
    {
        private Mock<IDataReader> mockDataReader = new Mock<IDataReader>();
      
        [SetUp]
        public void SetUp() { }

        [TearDown]
        public void TearDown()
        {
            mockDataReader.Reset();
        }

        #region UseSqlConnection 
        [Test]
        public void UseSqlConnection_CheckIfConnectionIsOpen_True()
        {
            Action<SqlConnection> action = (sqlConnection) => 
            {
                Assert.IsTrue(sqlConnection.State == ConnectionState.Open);
            };

            SqlHelper.UseSqlConnection(action);
        }
        #endregion

        #region IdToSqlCommandBuilder
        [Test]
        public void IdToSqlCommandBuilder_ValidArguments_ReturnValidSqlCommand()
        {
            //Arrange
            var id = RandomizerHelper.GetRandomInt();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            //Act
            var actual = SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }

        [Test]
        public void IdToSqlCommandBuilder_IdIsZero_ThrowArgumentNullException()
        {
            //Arrange
            var id = 0;
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection));
        }


        [Test]
        public void IdToSqlCommandBuilder_IdIsNegative_ThrowArgumentNullException()
        {
            //Arrange
            var id = RandomizerHelper.GetRandomNegativeInt();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection));
        }

        [Test]
        public void IdToSqlCommandBuilder_QueryIsNull_ThrowArgumentNullException()
        {
            //Arrange
            var id = RandomizerHelper.GetRandomInt();
            var query = default(string);
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection));
        }

        [Test]
        public void IdToSqlCommandBuilder_QueryIsWhiteSpace_ThrowArgumentNullException()
        {
            //Arrange
            var id = RandomizerHelper.GetRandomInt();
            var query = "    ";
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection));
        }

        [Test]
        public void IdToSqlCommandBuilder_SqlConnectionIsNull_ThrowArgumentNullException()
        {
            //Arrange
            var id = RandomizerHelper.GetRandomInt();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = default(SqlConnection);

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.IdToSqlCommandBuilder(id, query, sqlConnection));
        }
        #endregion

        #region ModelToSqlCommandBuilder
        [Test]
        public void ModelToSqlCommandBuilder_ValidArguments_ReturnVaidSqlCommandForEvent()
        {
            //Arrange
            var model = RandomizerHelper.GetNewRandomEvent();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query, sqlConnection);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
            expected.Parameters.Add("@Name", SqlDbType.NVarChar).Value = model.Name;
            expected.Parameters.Add("@Description", SqlDbType.NVarChar).Value = model.Description;
            expected.Parameters.Add("@Price", SqlDbType.Decimal).Value = model.Price;
            expected.Parameters.Add("@StudentPrice", SqlDbType.Decimal).Value = model.StudentPrice;
            expected.Parameters.Add("@Date", SqlDbType.DateTime).Value = model.Date;
            expected.Parameters.Add("@Place", SqlDbType.NVarChar).Value = model.Place;

            //Act
            var actual = SqlHelper.ModelToSqlCommandBuilder<Event>(model, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }

        [Test]
        public void ModelToSqlCommandBuilder_ValidArguments_ReturnVaidSqlCommandForStudent()
        {
            //Arrange
            var model = RandomizerHelper.GetNewRandomStudent();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query, sqlConnection);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
            expected.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;

            //Act
            var actual = SqlHelper.ModelToSqlCommandBuilder<Student>(model, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }

        [Test]
        public void ModelToSqlCommandBuilder_ValidArguments_ReturnVaidSqlCommandForTicket()
        {
            //Arrange
            var model = RandomizerHelper.GetNewRandomTicket();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query, sqlConnection);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
            expected.Parameters.Add("@EventId", SqlDbType.Int).Value = model.EventId;
            expected.Parameters.Add("@IsUsed", SqlDbType.Bit).Value = model.IsUsed;
            expected.Parameters.Add("@StudentId", SqlDbType.Int).Value = model.StudentId;
            expected.Parameters.Add("@StudentId", SqlDbType.Int).Value = model.StudentId;

            //Act
            var actual = SqlHelper.ModelToSqlCommandBuilder<Ticket>(model, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }

        [Test]
        public void ModelToSqlCommandBuilder_EventModelIsNull_ThrowArgumentIsNullException()
        {
            //Arrange
            var model = default(Event);
            var query = It.IsNotNull<string>();
            var sqlConnection = It.IsNotNull<SqlConnection>();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.ModelToSqlCommandBuilder<Event>(model, query, sqlConnection));
        }

        [Test]
        public void ModelToSqlCommandBuilder_StudentModelIsNull_ThrowArgumentIsNullException()
        {
            //Arrange
            var model = default(Student);
            var query = It.IsNotNull<string>();
            var sqlConnection = It.IsNotNull<SqlConnection>();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.ModelToSqlCommandBuilder<Student>(model, query, sqlConnection));
        }


        [Test]
        public void ModelToSqlCommandBuilder_TicketModelIsNull_ThrowArgumentIsNullException()
        {
            //Arrange
            var model = default(Ticket);
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.ModelToSqlCommandBuilder<Ticket>(model, query, sqlConnection));
            
        }

        [Test]
        public void ModelToSqlCommandBuilder_QueryIsNull_ThrowArgumentIsNullException()
        {
            //Arrange
            var model = RandomizerHelper.GetNewRandomEvent();
            var query = default(string);
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.ModelToSqlCommandBuilder<Event>(model, query, sqlConnection));
        }


        [Test]
        public void ModelToSqlCommandBuilder_QueryIsWhiteSpace_ThrowArgumentIsNullException()
        {
            //Arrange
            var model = RandomizerHelper.GetNewRandomEvent();
            var query = "    ";
            var sqlConnection = new SqlConnection();

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.ModelToSqlCommandBuilder<Event>(model, query, sqlConnection));
        }

        [Test]
        public void ModelToSqlCommandBuilder_SqlConnectionIsNull_ThrowArgumentIsNullException()
        {
            //Arrange
            var model = RandomizerHelper.GetNewRandomEvent();
            var query = RandomizerHelper.GetRandomString();
            SqlConnection sqlConnection = null;

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => SqlHelper.ModelToSqlCommandBuilder<Event>(model, query, sqlConnection));
        }

        [Test]
        public void ModelToSqlCommandBuilder_EventPropertiesAreDefaultValues_ReturnSqlCommandWithDefaultValues()
        {
            //Arrange
            var model = new Event();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query, sqlConnection);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = default(int);
            expected.Parameters.Add("@Name", SqlDbType.NVarChar).Value = default(string);
            expected.Parameters.Add("@Description", SqlDbType.NVarChar).Value = default(string);
            expected.Parameters.Add("@Price", SqlDbType.Decimal).Value = default(decimal);
            expected.Parameters.Add("@StudentPrice", SqlDbType.Decimal).Value = default(decimal);
            expected.Parameters.Add("@Date", SqlDbType.DateTime).Value = default(DateTime);
            expected.Parameters.Add("@Place", SqlDbType.NVarChar).Value = default(string);
            //Act
            var actual = SqlHelper.ModelToSqlCommandBuilder<Event>(model, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }

        [Test]
        public void ModelToSqlCommandBuilder_StudentPropertiesDefaultValues_ReturnSqlCommandWithDefaultValues()
        {
            //Arrange
            var model = new Student();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query, sqlConnection);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = default(int);
            expected.Parameters.Add("@Email", SqlDbType.NVarChar).Value = default(string);

            //Act
            var actual = SqlHelper.ModelToSqlCommandBuilder<Student>(model, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }

        [Test]
        public void ModelToSqlCommandBuilder_TicketPropertiesDefaultValues_ReturnSqlCommandWithDefaultValues()
        {
            //Arrange
            var model = new Ticket();
            var query = RandomizerHelper.GetRandomString();
            var sqlConnection = new SqlConnection();

            var expected = new SqlCommand(query, sqlConnection);
            expected.Parameters.Add("@Id", SqlDbType.Int).Value = default(int);
            expected.Parameters.Add("@EventId", SqlDbType.Int).Value = default(int);
            expected.Parameters.Add("@IsUsed", SqlDbType.Bit).Value = default(bool);
            expected.Parameters.Add("@StudentId", SqlDbType.Int).Value = default(int);

            //Act
            var actual = SqlHelper.ModelToSqlCommandBuilder<Ticket>(model, query, sqlConnection);

            //Assert
            AssertSqlCommandAreEqual(expected, actual);
        }
        #endregion

        #region SqlReaderToModelBuilder
        [Test]
        public void SqlReaderToModelBuilder_ValidSqlReader_ReturnCorrectEventModel()
        {
            //Arrange
            var expected = RandomizerHelper.GetNewRandomEvent();
            mockDataReader.Setup(x => x.GetInt32(0)).Returns(expected.Id);
            mockDataReader.Setup(x => x.GetString(1)).Returns(expected.Name);
            mockDataReader.Setup(x => x.GetString(2)).Returns(expected.Description);
            mockDataReader.Setup(x => x.GetDecimal(3)).Returns(expected.Price);
            mockDataReader.Setup(x => x.GetDecimal(4)).Returns(expected.StudentPrice);
            mockDataReader.Setup(x => x.GetDateTime(5)).Returns(expected.Date);
            mockDataReader.Setup(x => x.GetString(6)).Returns(expected.Place);

            //Act
            var actual = SqlHelper.SqlReaderToModelBuilder<Event>(mockDataReader.Object);

            //Assert
            AssertEventAreEqual(expected, actual);    
        }

        [Test]
        public void SqlReaderToModelBuilder_ValidSqlReader_ReturnCorrectStudentModel()
        {
            //Arrange
            var expected = RandomizerHelper.GetNewRandomStudent();
            mockDataReader.Setup(x => x.GetInt32(0)).Returns(expected.Id);
            mockDataReader.Setup(x => x.GetString(1)).Returns(expected.Email);

            //Act
            var actual = SqlHelper.SqlReaderToModelBuilder<Student>(mockDataReader.Object);

            //Assert
            AssertStudentAreEqual(expected, actual);
        }

        [Test] 
        public void SqlReaderToModelBuilder_ValidSqlReader_ReturnCorrectTIcketModel()
        {
            //Arrange
            var expected = RandomizerHelper.GetNewRandomTicket();
            mockDataReader.Setup(x => x.GetInt32(0)).Returns(expected.Id);
            mockDataReader.Setup(x => x.GetInt32(1)).Returns(expected.EventId);
            mockDataReader.Setup(x => x.GetBoolean(2)).Returns(expected.IsUsed);
            mockDataReader.Setup(x => x.GetInt32(3)).Returns(expected.StudentId);

            //Act
            var actual = SqlHelper.SqlReaderToModelBuilder<Ticket>(mockDataReader.Object);

            //Assert
            AssertTicketAreEqual(expected, actual);
        }

        [Test]
        public void SqlReaderToModelBuilder_SqlReaderIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => SqlHelper.SqlReaderToModelBuilder<Event>(null));
            Assert.Throws<ArgumentNullException>(() => SqlHelper.SqlReaderToModelBuilder<Student>(null));
            Assert.Throws<ArgumentNullException>(() => SqlHelper.SqlReaderToModelBuilder<Ticket>(null));
        }
        #endregion

        #region Assert Helpers
        private void AssertEventAreEqual(Event expected, Event actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.StudentPrice, actual.StudentPrice);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.Place, actual.Place);
        }

        private void AssertStudentAreEqual(Student expected, Student actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Email, actual.Email);
        }

        private void AssertTicketAreEqual(Ticket expected, Ticket actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.EventId, actual.EventId);
            Assert.AreEqual(expected.IsUsed, actual.IsUsed);
            Assert.AreEqual(expected.StudentId, actual.StudentId);
        }

        private void AssertSqlCommandAreEqual(SqlCommand expected, SqlCommand actual)
        {
            Assert.AreEqual(expected.CommandText, actual.CommandText);
            Assert.AreEqual(expected.Parameters.Count, actual.Parameters.Count);
            
            var count = expected.Parameters.Count == actual.Parameters.Count ? expected.Parameters.Count : -1;
            if(count == -1)
            {
                throw new AssertionException("Expected.Parameters and Actual.Parameters differs in size");
            }
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(expected.Parameters[i].ParameterName, actual.Parameters[i].ParameterName);
                Assert.AreEqual(expected.Parameters[i].Value, actual.Parameters[i].Value);
                Assert.AreEqual(expected.Parameters[i].SqlDbType, actual.Parameters[i].SqlDbType);
            }
              

        }
        #endregion
    }
}
