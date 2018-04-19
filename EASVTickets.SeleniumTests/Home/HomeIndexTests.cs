using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using System;

namespace EASVTickets.SeleniumTests.Home
{
    /// <summary>
    /// In order for these tests to work properly, make sure to always run
    /// the targeted application in DEBUG mode. 
    /// 
    /// The reason for this is the targeted application will return fake custom data
    /// when in debug mode, but when in RELEASE will hit an actual API.
    /// </summary>
    [TestFixture]
    public class HomeIndexTests
    {

        private IWebDriver _driver;
        private string _url = "http://localhost:53193";

        [SetUp]
        public void SetUp()
        {
            _driver = new PhantomJSDriver();
            _driver.Navigate().GoToUrl(_url);
        }

        [TearDown]
        public void TearDown()
        {
            if(_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [Test]
        public void Home_Index_EnsureTHereAreFiveElements()
        {
            //Arrange
            var expected = 5;

            //Act
            var subject = _driver.FindElements(By.ClassName("li-click"));
            var actual = subject.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Home_Index_EnsureIdExists(int id)
        {
            //Act 
            var subject = _driver.FindElements(By.Id(id.ToString()));
            var actual = subject.Count == 1;

            //Assert
            Assert.IsTrue(actual);
        }

        [TestCase(1, "Christmas Party")]
        [TestCase(2, "New Year's Eve")]
        [TestCase(3, "Graduation Party")]
        [TestCase(4, "Easter Holiday")]
        [TestCase(5, "Second Halloween")]
        public void Home_Index_EnsureNameIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/div[1]/h2"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(1, "Come and Celebrate Christmas Eve with us!!")]
        [TestCase(2, "You like beautiful fireworks? We have a planned show with experts when the clock hits. After that, you will buy able to buy your own rockets for very cheap and enjoy the evening with us")]
        [TestCase(3, "Come and celebrate student who finished their education and will move into the real world")]
        [TestCase(4, "We'll paint eggs!!!!")]
        [TestCase(5, "We like halloween, so let's celebrate it twice a year")]
        public void Home_Index_EnsureDescriptionIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/div[1]/p[1]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "Location: EASV C101")]
        [TestCase(2, "Location: EASV Cafeteria")]
        [TestCase(3, "Location: Spangsbjerg Kirkevej 103")]
        [TestCase(4, "Location: Midtown Esbjerg")]
        [TestCase(5, "Location: Spooky town")]
        public void Home_Index_EnsureLocationIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/div[1]/p[2]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "50 DKK")]
        [TestCase(2, "100 DKK")]
        [TestCase(3, "200 DKK")]
        [TestCase(4, "500 DKK")]
        [TestCase(5, "100 DKK")]
        public void Home_Index_EnsurePriceIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/div[2]/ul/li[2]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "0 DKK")]
        [TestCase(2, "100 DKK")]
        [TestCase(3, "0 DKK")]
        [TestCase(4, "250 DKK")]
        [TestCase(5, "100 DKK")]
        public void Home_Index_EnsureStudentPriceIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/div[2]/ul/li[5]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "2017")]
        [TestCase(2, "2017")]
        [TestCase(3, "2018")]
        [TestCase(4, "2018")]
        [TestCase(5, "2018")]
        public void Home_Index_EnsureYearDateIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/time/span[3]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "DEC")]
        [TestCase(2, "DEC")]
        [TestCase(3, "JAN")]
        [TestCase(4, "FEB")]
        [TestCase(5, "APR")]
        public void Home_Index_EnsureMonthDateIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/time/span[2]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "24")]
        [TestCase(2, "31")]
        [TestCase(3, "27")]
        [TestCase(4, "15")]
        [TestCase(5, "27")]
        public void Home_Index_EnsureDayDateIsCorrect(int id, string expected)
        {
            //Act
            var subject = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]/time/span[1]"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Home_Index_EnsureEventIsClickable(int id)
        {
            //Arrange
            var expected = $"{_url}/Home/SingleTicket/{1}";

            //Act
            var eventElement = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]"));
            eventElement.Click();
            var actual = _driver.Url;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
