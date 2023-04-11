using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Withywoods.Selenium;

namespace ToDo.App.IntegrationTests.PageTests;

public sealed class MainLayoutTests : SeleniumTestBase, IClassFixture<ToDoAppFactory>
{
    private readonly string _webUrl = "https://localhost:7112";
    
    public MainLayoutTests(ToDoAppFactory factory) : base(new WebDriverOptions())
    {
        factory.HostUrl = _webUrl;
        factory.CreateDefaultClient();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("todos")]
    public void GoToPageAndToggleBurgerMenu_ShouldToggleState(string uri)
    {
        try
        {
            WebDriver.Navigate().GoToUrl($"{_webUrl}/{uri}");

            WebDriver.FindElement(By.TagName("aside")).GetAttribute("class")
                .Contains("mud-drawer--open")
                .Should()
                .BeTrue();

            new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(100))
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(By.ClassName("mud-button-root"))))
                .Click();

            WebDriver.FindElement(By.TagName("aside")).GetAttribute("class")
                .Contains("mud-drawer--closed")
                .Should()
                .BeTrue();

            TakeScreenShot(nameof(GoToPageAndToggleBurgerMenu_ShouldToggleState));
        }
        catch
        {
            TakeScreenShot(nameof(GoToPageAndToggleBurgerMenu_ShouldToggleState));
            throw;
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("todos")]
    public void GoToPageAndClickMenuBarLinks_ShouldBeClickable(string uri)
    {
        try
        {
            WebDriver.Navigate().GoToUrl($"{_webUrl}/{uri}");
            Thread.Sleep(TimeSpan.FromMilliseconds(300));

            new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(300))
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(By.CssSelector("a[href='https://github.com/MudBlazor/MudBlazor/']"))))
                .Click();
            
            new WebDriverWait(WebDriver, TimeSpan.FromMilliseconds(300))
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(By.CssSelector("a[href='https://mudblazor.com/']"))))
                .Click();
            
            TakeScreenShot(nameof(GoToPageAndClickMenuBarLinks_ShouldBeClickable));
        }
        catch
        {
            TakeScreenShot(nameof(GoToPageAndClickMenuBarLinks_ShouldBeClickable));
            throw;
        }
    }
}