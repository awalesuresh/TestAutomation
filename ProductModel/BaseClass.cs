using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DataModels;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace ProductModel
{
    public class BaseClass
    {
        protected readonly IConfiguration _configuration;
        protected static IWebDriver webDriver;
        public IWebDriver Driver
        {
            get
            {
                return webDriver;
            }
        }
        public static TestSettings TestSettings { get; set; }
        protected static ExtentTest TestLogger { get; set; }
        protected static ExtentReports Log;
        public BaseClass()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            TestSettings = new TestSettings() { Browser = _configuration["TestSettings:Browser"], SearchText = _configuration["TestSettings:SearchText"], URL = _configuration["TestSettings:URL"] };
        }

        /// <summary>
        /// This method will lauch the given browser and navigate to URL
        /// </summary>
        /// <param name="browser">Browser Name</param>
        public void LaunchBrowser(BrowserTypes browser)
        {

            switch (browser)
            {
                case BrowserTypes.Chrome:
                    webDriver = new ChromeDriver();
                    TestLogger.Log(Status.Pass, $"{browser} is launched");

                    webDriver.Manage().Window.Maximize();
                    webDriver.Navigate().GoToUrl(TestSettings.URL);
                    TestLogger.Log(Status.Pass, $"Navigated to URL - {TestSettings.URL}");
                    WaitForLoading();
                    break;

                case BrowserTypes.FireFox:
                    webDriver = new FirefoxDriver();
                    webDriver.Manage().Window.Maximize();
                    webDriver.Navigate().GoToUrl(TestSettings.URL);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// This method will capture screenshot
        /// </summary>
        /// <returns>return Base64EncodedString </returns>
        public string ScreenCaptureAsBase64String()
        {
            ITakesScreenshot takeScreenshot = (ITakesScreenshot)webDriver;
            Screenshot screenshot = takeScreenshot.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }

        /// <summary>
        /// This method will close the browser
        /// </summary>
        public void CloseBrowser()
        {
            Log.Flush();

            if (webDriver != null)
                webDriver.Quit();
        }

        /// <summary>
        /// Method for waiting for Page load 
        /// </summary>
        protected void WaitForLoading(int timeoutInSecs = 30)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, timeoutInSecs));
                wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
            }
            catch (Exception ex)
            {
                TestLogger.Log(Status.Error, ex.GetType().ToString() + " occurred. Waiting for " + timeoutInSecs + "seconds");
            }
        }

        /// <summary>
        /// This method will initialize the logger 
        /// </summary>
        /// <param name="filePath">Path of Log </param>
        /// <param name="fileName">Log File Name</param>
        /// <param name="testContext"></param>
        /// <returns></returns>
        public static ExtentTest StartLogging(string filePath, string fileName, TestContext testContext)
        {
            Log = new ExtentReports();

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var reporter = new ExtentHtmlReporter(Path.Combine(filePath, fileName));
            reporter.Config.ReportName = fileName;
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            Log.AttachReporter(reporter);

            Log.AddSystemInfo("Machine", Environment.MachineName);
            Log.AddSystemInfo("OS", Environment.OSVersion.VersionString);

            TestLogger = Log.CreateTest(testContext.TestName);
            return TestLogger;
        }
        public enum BrowserTypes
        {
            Chrome,
            FireFox
        }

    }
}
