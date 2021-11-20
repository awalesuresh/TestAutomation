using AventStack.ExtentReports;
using DataModels;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestAutomation
{
    [TestClass]
    public class TestBase
    {
        protected readonly IConfiguration _configuration;
        protected TestSettings testSettings;
        protected BaseClass BaseClass;
        protected TestApp TestApp;
        public static TestContext testContext { get; set; }
        protected ExtentTest Logger;
        private readonly string filePath = $@"D:\TestAutomation\Test{DateTime.Now.ToString("ddMMhhss")}";
        public TestBase()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            testContext = context;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            BaseClass = new BaseClass();
            TestApp = new TestApp();
            testSettings = BaseClass.TestSettings;
            Logger = BaseClass.StartLogging(filePath, "TestRun.html", testContext);

            Logger.Log(Status.Info, $"Execution started for Test - {testContext.TestName}");
            BaseClass.LaunchBrowser((BaseClass.BrowserTypes)Enum.Parse(typeof(BaseClass.BrowserTypes), testSettings.Browser, true));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            BaseClass.CloseBrowser();
        }

    }
}
