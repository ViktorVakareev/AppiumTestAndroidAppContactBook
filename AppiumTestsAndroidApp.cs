using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Runtime.Serialization;

namespace AppiumTestAndroidAppContactBook
{
    public class AppiumTestsAndroidApp
    {

        private AndroidDriver<AndroidElement> driver;
        private const string AppiumServerUrl="http://[::1]:4723/wd/hub";  // assign appium server Host(IPv6 ::1) and Port(4723) /wd/hub is by default
        private const string AppPath = @"D:\Java_Studies\QA\QA_Automation\04.1 Consultation\contactbook-androidclient.apk";
        private const string ContactBookUrl = "https://contactbook.nakov.repl.co/api";

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("platformName","Android");
            appiumOptions.AddAdditionalCapability("app",
                AppPath); //path to apk, @ - so we need not to escape special characters
            

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUrl),appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);  // time for loading
        }

        [Test]
        public void TestContactBookSearch()
        {
            var contactBookUrl = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            contactBookUrl.Clear();
            contactBookUrl.SendKeys(ContactBookUrl);  // Introduce constant -> alt+Enter

            var connectButton=driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();

            var textBoxSearch = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            textBoxSearch.Clear();
            textBoxSearch.SendKeys("albert");

            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();

            var firstName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewFirstName"));
            var lastName = driver.FindElement(By.Id("contactbook.androidclient:id/textViewLastName"));

            Assert.AreEqual("Albert", firstName.Text);
            Assert.AreEqual("Einstein", lastName.Text);
            


        }

        [OneTimeTearDown]
        public void Shutdown()
        {
            driver.Quit();
        }
    }
}