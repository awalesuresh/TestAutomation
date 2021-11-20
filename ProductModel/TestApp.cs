namespace ProductModel
{
    public class TestApp : BaseClass
    {
        public SearchPage searchPage;

        public SearchPage SearchPage
        {
            get
            {
                string searchEngine = TestSettings.URL.Split('.')[1];

                switch (searchEngine.ToLower())
                {
                    case "google":
                        searchPage = new GoogleSearchPage();
                        break;

                    case "yahoo":
                        searchPage = new YahooSearchPage();
                        break;

                    default:
                        searchPage = new GoogleSearchPage();
                        break;
                }
                return searchPage;
            }
        }
    }
}
