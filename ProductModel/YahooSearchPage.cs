using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModel
{
    public class YahooSearchPage : SearchPage
    {
        #region Properties
        protected override IWebElement txtSearch
        {
            get
            {
                return Driver.FindElement(By.Name("p"));
            }
        }
        protected override IWebElement btnSearch
        {
            get
            {
                return Driver.FindElement(By.ClassName("mag-glass"));
            }
        }

        #endregion
        #region Methods
        public override void SetSearchText(string searxhText)
        {

            TestLogger.Log(Status.Info, $"Setting search text as {searxhText}");
            txtSearch.SendKeys(searxhText);
            txtSearch.SendKeys(Keys.Escape);

            TestLogger.Log(Status.Info, "Clicking on Search button");
            try
            {
                btnSearch.Click();
            }
            catch (Exception ex)
            {
                txtSearch.SendKeys(Keys.Enter);
            }

            WaitForLoading();
        }
        #endregion
    }
}
