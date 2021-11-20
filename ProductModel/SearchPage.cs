using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace ProductModel
{
    public abstract class SearchPage : BaseClass
    {
        #region Properties
        protected abstract IWebElement txtSearch { get; }

        protected abstract IWebElement btnSearch { get; }
       

        protected IWebElement link
        {
            get
            {



                return Driver.FindElement(By.PartialLinkText(TestSettings.SearchText));
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// This method will set the search  text and click on search button
        /// </summary>
        /// <param name="searxhText">Search String</param>
        public abstract void SetSearchText(string searxhText);
        

        /// <summary>
        /// This method will check link is displayed or not
        /// </summary>
        /// <returns>Returns true if displayed else false</returns>
        public bool VerifyLinkDisplayed()
        {
            return link.Displayed;
        }
        #endregion
    }
}
