using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;

namespace ProductModel
{
    public class GoogleSearchPage : SearchPage
    {
        #region Properties
        protected override IWebElement txtSearch
        {
            get
            {
                return Driver.FindElement(By.Name("q"));
            }
        }
        protected override IWebElement btnSearch
        {
            get
            {
                return Driver.FindElement(By.Name("btnK"));
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
