using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace EASVTickets.SeleniumTests.Home
{
    [TestFixture]
    class HomeSingleTicketTests
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
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }


        [TestCase(1, "Christmas Party")]
        [TestCase(2, "New Year's Eve")]
        [TestCase(3, "Graduation Party")]
        [TestCase(4, "Easter Holiday")]
        [TestCase(5, "Second Halloween")]
        public void Home_SingleTicket_EnsureNameIsCorrect(int id, string expected)
        {
            //Act
            var menuItem = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]"));
            menuItem.Click();

            var subject = _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/h2"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "24-12-2017 00:00")]
        [TestCase(2, "31-12-2017 20:00")]
        [TestCase(3, "27-01-2018 00:00")]
        [TestCase(4, "15-02-2018 00:00")]
        [TestCase(5, "27-04-2018 00:00")]
        public void Home_SingleTicket_EnsureDateIsCorrect(int id, string expected)
        {
            //Act
            var menuItem = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]"));
            menuItem.Click();

            var subject = _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/h4"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "Come and Celebrate Christmas Eve with us!!")]
        [TestCase(2, "You like beautiful fireworks? We have a planned show with experts when the clock hits. After that, you will buy able to buy your own rockets for very cheap and enjoy the evening with us")]
        [TestCase(3, "Come and celebrate student who finished their education and will move into the real world")]
        [TestCase(4, "We'll paint eggs!!!!")]
        [TestCase(5, "We like halloween, so let's celebrate it twice a year")]
        public void Home_SingleTicket_EnsureDescriptionIsCorrect(int id, string expected)
        {
            //Act
            var menuItem = _driver.FindElement(By.XPath($"//*[@id=\"{id}\"]"));
            menuItem.Click();

            var subject = _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div[1]/span"));
            var actual = subject.Text;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
