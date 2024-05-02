using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Program
{
    public class ChromeWebDriver
    {
        private ChromeWebDriver() { }
        private static ChromeWebDriver? instance;

        private static ChromeDriver driver;
        private static IJavaScriptExecutor jsExecutor;

        public static ChromeWebDriver Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChromeWebDriver();
                    Init();
                }

                return instance!;
            }
        }

        public static IJavaScriptExecutor getJsExecutor()
        {
            return jsExecutor;
        }

        public void Navigate(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement FindElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));

            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));

            return element;
        }

        public IWebElement FindShadowElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var searchContext = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//mymsc-instantquote-app"))).GetShadowRoot();

            SearchContextWait searchContextwait = new SearchContextWait(searchContext, TimeSpan.FromMinutes(1));
            var element = searchContextwait.Until(SearchContextExpectedConditions.ElementIsVisible(by));

            return element;
        }

        private static void Init()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("--lang=en-Us");
            chromeOptions.AddArguments("--incognito"); // Gizli mod
            chromeOptions.AddArguments("--no-sandbox");

            driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions, TimeSpan.FromMinutes(2));
            driver.Manage().Window.Maximize();

            jsExecutor = driver;
        }
    }
}
