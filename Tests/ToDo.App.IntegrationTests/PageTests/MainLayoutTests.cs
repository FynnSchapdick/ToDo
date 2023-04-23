namespace ToDo.App.IntegrationTests.PageTests;

public sealed class MainLayoutTests : SeleniumTestBase, IClassFixture<ToDoAppFactory>
{
    private readonly string _webUrl = "https://localhost:7112";
    private readonly TimeSpan _waitTime = TimeSpan.FromMilliseconds(900);
    
    public MainLayoutTests(ToDoAppFactory factory) : base(new WebDriverOptions())
    {
        factory.HostUrl = _webUrl;
        factory.CreateDefaultClient();
    }

    [Fact]
    public void ClickMenuBarLinksOnPage_ShouldBeClickable()
    {
        try
        {
            WebDriver.Navigate().GoToUrl($"{_webUrl}");
            Thread.Sleep(_waitTime);

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(By.CssSelector("a[href='https://github.com/fynnschapdick/todo/']"))))
                .Click();
            
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(By.CssSelector("a[href='https://mudblazor.com/']"))))
                .Click();
            
            TakeScreenShot(nameof(ClickMenuBarLinksOnPage_ShouldBeClickable));
        }
        catch
        {
            TakeScreenShot(nameof(ClickMenuBarLinksOnPage_ShouldBeClickable));
            throw;
        }
    }
}