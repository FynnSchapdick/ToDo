using FluentAssertions;
using Withywoods.Selenium;

namespace ToDo.App.IntegrationTests.PageTests;

public sealed class IndexPageTests : SeleniumTestBase, IClassFixture<ToDoAppFactory>
{
    private readonly string _webUrl = "https://localhost:7112";

    public IndexPageTests(ToDoAppFactory factory) : base(new WebDriverOptions())
    {
        factory.HostUrl = _webUrl;
        factory.CreateDefaultClient();
    }
    
    [Fact]
    public void GoToIndexPage_ShouldFindIndexTitle()
    {
        try
        {
            // Arrange & Act
            WebDriver.Navigate().GoToUrl(_webUrl);

            // Assert
            WebDriver.Title.Should().Be("Index");
            TakeScreenShot(nameof(GoToIndexPage_ShouldFindIndexTitle));
        }
        catch
        {
            TakeScreenShot(nameof(GoToIndexPage_ShouldFindIndexTitle));
            throw;
        }
    }
}