using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TicketStore.Web.Tests
{
    public class UiTestFixture : IDisposable
    {
        public Lazy<ChromeDriver> WebDriver;

        public UiTestFixture()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            WebDriver = new Lazy<ChromeDriver>();
        }

        public IWebDriver Browser()
        {
            return this.WebDriver.Value;
        }

        public void Dispose()
        {
            WebDriver.Value.Quit();
        }
    }
}
