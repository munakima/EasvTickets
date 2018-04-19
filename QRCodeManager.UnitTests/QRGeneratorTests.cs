using NUnit.Framework;
using QRCodeManager.Common;
using QRCodeManager.UnitTests.Helpers;
using System;
using System.Drawing;

namespace QRCodeManager.UnitTests
{
    [TestFixture]
    public class QRGeneratorTests
    {
        [SetUp]
        public void SetUp() { }

        [TearDown]
        public void TearDown() { }

        #region CreateQRCodeAsBitmap
        [Test]
        public void CreateQRCodeAsBitmap_ReturnValue_IsNotNull()
        {
            //Arrange
            var ticketId = 1;

            //Act
            var actual = QRGenerator.CreateQRCodeAsBitmap(ticketId);

            //Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CreateQRCodeAsBitmap_ReturnValue_IsCorrectBitmap()
        {
            //Arrange
            var ticketId = 1;
            var expected = QRGenereatorHelper.GetBitmap_Id1();

            //Act
            var actual = QRGenerator.CreateQRCodeAsBitmap(ticketId);

            //Assert
            AssertBitmap(actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void CreateQRCodeAsBitmap_InputLessThan1_ThrowException(int ticketId)
        { 
            //Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => QRGenerator.CreateQRCodeAsBitmap(ticketId));
        }
        #endregion

        #region CreateQRCodeAsByteArray
        [Test]
        public void CreateQRCodeAsByteArray_ReturnValie_IsNotNull()
        {
            //Arrange
            var ticketId = 1;

            //Act
            var actual = QRGenerator.CreateQRCodeAsByteArray(ticketId);

            //Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CreateQRCodeAsByteArray_ReturnValue_IsCorrectArray()
        {
            //Arrange
            var expected = QRGenereatorHelper.GetByteArray_Id1();
            var ticketId = 1;

            //Act
            var actual = QRGenerator.CreateQRCodeAsByteArray(ticketId);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void CreateQRCodeAsByteArray_InputLessThan1_ThrowException(int ticketId)
        {
            //Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => QRGenerator.CreateQRCodeAsByteArray(ticketId));
        }

        #endregion

        #region CreateQRCodeAsBase64String
        [Test]
        public void CreateQRCodeAsBase64String_Returnvalue_IsNotNull()
        {
            //Arrange
            var ticketId = 1;

            //Act
            var actual = QRGenerator.CreateQRCodeAsBase64String(ticketId);

            //Assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CreateQRCodeAsBase64String_ReturnValue_IsCorrectString()
        {
            //Arrange
            var expected = QRGenereatorHelper.GetBase64String_Id1();
            var ticketId = 1;

            //Act
            var actual = QRGenerator.CreateQRCodeAsBase64String(ticketId);

            //Assert 
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void CreateQRCodeAsBase64String_InputLessThan1_ThrowException(int ticketId)
        {


            //Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => QRGenerator.CreateQRCodeAsBase64String(ticketId));
        }

        #endregion
        
        #region Assert Helpers
        public void AssertBitmap(Bitmap actual)
        {
            var expectedByteArray = QRGenereatorHelper.GetByteArray_Id1();
            var actualByteArray = QRGenereatorHelper.BitmapToByteArray(actual);
            CollectionAssert.AreEqual(expectedByteArray, actualByteArray);
        }

        #endregion
    }
}
