using TestProject;
using OpenQA.Selenium;

public class Program
{
    private static ChromeWebDriver driver = ChromeWebDriver.Instance;

    static public void Main()
    {
        driver.Navigate("yoursiteurl");

        // Reject Cookies
        var rejectCookieButton = driver.FindElement(By.Id("onetrust-reject-all-handler"));
        rejectCookieButton.Click();

        // User Name
        var userNameInput = driver.FindElement(By.Id("UserName"));
        userNameInput.SendKeys("userrrr");

        // Next Button Click
        var nextButton = driver.FindElement(By.LinkText("Next"));
        nextButton.Click();

        // Password
        var passwordInput = driver.FindElement(By.Id("password"));
        passwordInput.SendKeys("passssss");

        // Login Button
        var loginButton = driver.FindElement(By.Id("next"));
        loginButton.Click();

        var input1 = driver.FindShadowElement(By.Name("model1.displayName"));
        input1.SendKeys("Bmw");

        var input1SelectedOption = driver.FindShadowElement(By.Id("option-0"));
        input1SelectedOption.Click();

        var input2 = driver.FindShadowElement(By.Name("model2.displayName"));
        input2.SendKeys("Audi");

        var input2SelectedOption = driver.FindShadowElement(By.Id("option-0"));
        input2SelectedOption.Click();


    }
}
