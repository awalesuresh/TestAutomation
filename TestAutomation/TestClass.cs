using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestAutomation
{
    [TestClass]
    public class TestClass : TestBase
    {

        [TestMethod, TestCategory("Smoke")]
        public void TestMethod_SearchText()
        {
            try
            {
                //Set Search Text
                TestApp.SearchPage.SetSearchText(testSettings.SearchText);

                //Verify link with search text is displayed
                bool linkDisplayed = TestApp.SearchPage.VerifyLinkDisplayed();
                if (linkDisplayed)
                {
                    Logger.Pass($"Link with search Text '{testSettings.SearchText}' is displayed");
                    Logger.AddScreenCaptureFromBase64String(TestApp.SearchPage.ScreenCaptureAsBase64String(), "Link displayed");
                }
                else
                {
                    Logger.Fail($"Link with search Text '{testSettings.SearchText}' is NOT displayed");
                    Logger.AddScreenCaptureFromBase64String(TestApp.SearchPage.ScreenCaptureAsBase64String(), "Link NOT displayed");
                }
            }
            catch (Exception ex)
            {
                Logger.Fail($"Test {testContext.TestName} failed. Failure reason - {ex.ToString()}");
                Assert.Fail(ex.ToString());
            }
        }
    }
}
